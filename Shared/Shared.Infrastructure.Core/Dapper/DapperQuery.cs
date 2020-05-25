/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Order.Infrastructure.Repository.Dapper
/// 文件名称    ：DapperRepository.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2019/12/3 9:39:43 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************

using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Shared.Domain.Abstractions;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Dapper
{
    public class DapperQuery<TEntity> : IDapperQuery<TEntity> where TEntity :class, IEntity<Guid>, IAggregateRoot
    {
        private readonly IConfiguration _configuration;
        private readonly string constr;
        private readonly DbContext _context;
        public DapperQuery(IConfiguration Configuration)
        {
            _configuration = Configuration;
            constr = _configuration.GetConnectionString("DefaultConnection");
        }
        public DbConnection Connection => _context.Database.GetDbConnection();

        public DbTransaction ActiveTransaction => _context.Database.CurrentTransaction?.GetDbTransaction();

        /// <summary>
        /// 查询    
        /// </summary>
        /// <returns></returns>
        public async Task<TEntity> QueryAsync(string sql)
        {
            using var connection = new MySqlConnection(constr);
            connection.Open();
            var result = await connection.QueryAsync<TEntity>(sql);
            return result.FirstOrDefault();
        }
    }
}
