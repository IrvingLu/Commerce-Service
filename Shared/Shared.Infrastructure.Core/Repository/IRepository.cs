/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Order.Core.Repository
/// 文件名称    ：IRepository.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2019/11/30 10:32:18 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************

using Shared.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Core.Repository
{
    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        bool Remove(Entity entity);
        Task<bool> RemoveAsync(Entity entity);
    }


    public interface IRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : Entity<TKey>, IAggregateRoot
    {
        bool Delete(TKey id);
        Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);
        TEntity Get(TKey id);
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
