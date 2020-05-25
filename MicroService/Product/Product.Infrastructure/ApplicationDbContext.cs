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

using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Core;

namespace Product.Infrastructure.EntityFrameworkCore
{
    public class ApplicationDbContext : EFContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator, ICapPublisher capBus) : base(options, mediator, capBus)
        {

        }
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 注册领域模型与数据库的映射关系
            //modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            #endregion
            base.OnModelCreating(modelBuilder);
        }

      //private  class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Core.Domain.Order>
      //  {
      //      public void Configure(EntityTypeBuilder<Core.Domain.Order> builder)
      //      {
      //          builder.HasKey(p => p.Id);
      //      }
      //  }
    }
}
