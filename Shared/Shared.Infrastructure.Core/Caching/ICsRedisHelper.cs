/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Shared.Infrastructure.Core.Caching
/// 文件名称    ：ICsRedisHelper.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2020/3/26 15:15:53 
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
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Caching
{
    public interface ICsRedisHelper
    {
        Task DeleteAsync(string key);
        Task DeleteAsync(string key, string field);
        Task<T> GetAsync<T>(string key, Func<T> acquire);
        Task<T> GetAsync<T>(string key);
        Task<T> GetAsync<T>(string key, string field);
        Task<Dictionary<string, T>> GetHashAllAsync<T>(string key);
        Task<bool> KeyExistsAsync(string key);
        Task SetAsync(string key, string value, int cacheTime = 0);
        Task SetAsync(string key, string field, string value, int cacheTime = 0);
    }
}
