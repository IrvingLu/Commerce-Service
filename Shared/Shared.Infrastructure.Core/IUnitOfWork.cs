/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Shared.Infrastructure.Core.Repository
/// 文件名称    ：IUnitOfWork.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2020/3/23 15:33:35 
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
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}
