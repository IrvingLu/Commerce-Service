
/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Order.Infrastructure.Dapper
/// 文件名称    ：IDapperRepository.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2019/12/3 9:38:05 
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
using System.Threading.Tasks;


namespace Shared.Infrastructure.Core.Dapper
{
    public interface IDapperQuery<TEntity> where TEntity : class, IAggregateRoot
    {
        Task<TEntity> QueryAsync(string sql);
    }
}
