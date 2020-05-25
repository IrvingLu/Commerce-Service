/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Order.Service
/// 文件名称    ：CreateOrderCommandHandler.cs
/// =================================
/// 创 建 者    ：鲁岩奇
/// 创建日期    ：2019/12/4 11:14:39 
/// 功能描述    ：
/// 使用说明    ：
/// =================================
/// 修改者    ：
/// 修改日期    ：
/// 修改内容    ：
/// =================================
///
/// ***********************************************************************

using AutoMapper;
using MediatR;
using Order.Infrastructure.Repositories;
using Order.Web.Application.Command;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Web.Application.CommandHandler
{
    public class OrderCommandHandler : IRequestHandler<CreateCommand, Unit>, IRequestHandler<UpdateOrderCommand, Unit>
    {
        #region Fileds
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public OrderCommandHandler(IOrderRepository orderRepository,IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<Core.Domain.Order>(request);
            await _orderRepository.AddAsync(result);
            await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return new Unit();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
