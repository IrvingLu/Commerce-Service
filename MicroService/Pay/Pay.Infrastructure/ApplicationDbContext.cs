/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：User.Infrastructure.Data
/// 文件名称    ：UserDbContext.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2019/10/28 10:08:18 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************

using Microsoft.EntityFrameworkCore;
using Pay.Core.Domain;

namespace Pay.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<PayRecord> PayRecord { get; set; }
    }
}
