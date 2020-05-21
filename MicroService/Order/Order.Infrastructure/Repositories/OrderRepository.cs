/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Order.Infrastructure.Repositories
/// 文件名称    ：OrderRepository.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2020/3/24 14:38:12 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************

using Order.Infrastructure.EntityFrameworkCore;
using Order.Infrastructure.Repository;
using System;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Core.Domain.Order, Guid, ApplicationDbContext>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
