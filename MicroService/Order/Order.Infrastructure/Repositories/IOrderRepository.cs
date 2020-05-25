/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Order.Infrastructure.Repositories
/// 文件名称    ：IOrderRepository.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2020/3/24 14:38:05 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************

using Shared.Infrastructure.Core.Repository;
using System;

namespace Order.Infrastructure.Repositories
{
    public interface IOrderRepository : IRepository<Core.Domain.Order, Guid>
    {

    }
}
