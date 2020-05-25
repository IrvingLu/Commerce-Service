/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Shared.Infrastructure.Core.MongoDb
/// 文件名称    ：MongodbClient.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2020/3/26 15:49:27 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************

using MongoDB.Driver;
using Shared.Infrastructure.Core.MongoDb.Models;

namespace Shared.Infrastructure.Core.MongoDb
{
    public static class MongodbClient<T> where T : class
    {
        #region +MongodbInfoClient 获取mongodb实例
        /// <summary>
        /// 获取mongodb实例
        /// </summary>
        /// <param name="host">连接字符串，库，表</param>
        /// <returns></returns>
        public static IMongoCollection<T> MongodbInfoClient(MongodbHost host)
        {
            var client = new MongoClient(host.Connection);
            var dataBase = client.GetDatabase(host.DataBase);
            return dataBase.GetCollection<T>(string.IsNullOrEmpty(host.Table) ? typeof(T).Name : host.Table);
        }

        #endregion
    }
}
