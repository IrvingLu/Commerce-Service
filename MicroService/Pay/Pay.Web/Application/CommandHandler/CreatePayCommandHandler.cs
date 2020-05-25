/// ***********************************************************************
///
/// =================================
/// CLR版本    ：4.0.30319.42000
/// 命名空间    ：Pay.Service
/// 文件名称    ：CreatePayCommandHandler.cs
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

using MediatR;
using Pay.Web.Application.Command;
using System.Threading;
using System.Threading.Tasks;

namespace Pay.Web.Application.CommandHandler
{
    public class CreatePayCommandHandler : IRequestHandler<CreatePayCommand, Unit>
    {
        #region Fileds
        //private readonly IRepository<PayRecord> _PayRepository;
        //private readonly IMapper _mapper;
        //private readonly ICapPublisher _capPublisher;
        #endregion


        #region Ctor
        public CreatePayCommandHandler()
        {
     
        }


        #endregion

        public async Task<Unit> Handle(CreatePayCommand request, CancellationToken cancellationToken)
        {
            //var result = _mapper.Map<PayRecord>(request);
            //_capPublisher.Publish("order.services.change.status", new OrderChangeEvents
            //{
            //    OrderId = Guid.Parse("5CDC7B9C-3151-4643-0280-08D7786BAA39")
            //});
            //await _PayRepository.InsertAsync(result);
            return new Unit();
        }
    }
}
