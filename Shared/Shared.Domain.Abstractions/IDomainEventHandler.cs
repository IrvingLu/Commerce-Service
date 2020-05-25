/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Shared.Domain.Abstractions
/// 文件名称    ：IDomainEventHandler.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2020/3/23 15:03:30 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************

using MediatR;

namespace Shared.Domain.Abstractions
{
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        //这里我们使用了INotificationHandler的Handle方法来作为处理方法的定义
        //Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}
