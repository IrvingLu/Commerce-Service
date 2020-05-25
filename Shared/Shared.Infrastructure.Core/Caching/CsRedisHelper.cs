/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Shared.Infrastructure.Core.Caching
/// 文件名称    ：CsRedisHelper.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2020/3/26 15:15:20 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Caching
{
    public class CsRedisHelper : ICsRedisHelper
    {
        #region Base
        /// <summary>
        /// 检查给定Key是否存在
        /// </summary>
        /// <param name="key">redis key</param>
        public async Task<bool> KeyExistsAsync(string key)
        {
            return await RedisHelper.ExistsAsync(key);
        }

        #endregion

        #region String

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        public async Task SetAsync(string key, string value, int cacheTime = 0)
        {
            if (RedisHelper.Exists(key))
            {
                await RedisHelper.DelAsync(key);
            }
            await RedisHelper.SetAsync(key, value, cacheTime);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string key)
        {
            await RedisHelper.DelAsync(key);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key)
        {
            return await RedisHelper.GetAsync<T>(key);
        }
        /// <summary>
        /// 获取缓存（如果没有直接设置）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="acquire"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key, Func<T> acquire)
        {
            //异步进行redis查询，增大吞吐量        
            if (RedisHelper.Exists(key))
            {
                return RedisHelper.Get<T>(key);
            }

            var result = acquire();
            await RedisHelper.SetAsync(key, result, CacheDefaults.CacheTime);
            return result;
        }
        #endregion

        #region Hash


        /// <summary>
        /// 写入缓存(hash)
        /// 注意：缓存过期时间手动设置随机值，防止所有key同一时间大面积失效，造成缓存雪崩！！！
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        /// <returns></returns>
        public async Task SetAsync(string key, string field, string value, int cacheTime = 0)
        {
            await RedisHelper.HSetAsync(key, field, value);
            if (cacheTime != 0)
            {
                var time = new TimeSpan(DateTime.Now.Ticks + cacheTime);
                var cacheMinutes = TimeSpan.FromMinutes(cacheTime);
                await RedisHelper.ExpireAsync(key, time + cacheMinutes);
            }
        }

        /// <summary>
        /// 删除缓存(hash)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string key, string field)
        {
            await RedisHelper.HDelAsync(key, field);
        }


        /// <summary>
        /// 获取缓存数据(hash)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, T>> GetHashAllAsync<T>(string key)
        {
            return await RedisHelper.HGetAllAsync<T>(key);
        }

        /// <summary>
        /// 获取缓存数据(hash)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key, string field)
        {
            return await RedisHelper.HGetAsync<T>(key, field);
        }

        #endregion
    }
}
