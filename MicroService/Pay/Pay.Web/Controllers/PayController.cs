using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pay.Web.Application.Command;
using System.Threading.Tasks;

namespace Pay.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class PayController : BaseController
    {
        //private readonly IMediator _mediator;
        //public PayController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}
        ///// <summary>
        ///// 微信支付
        ///// </summary>
        ///// <param name="PayCreateDto"></param>
        ///// <returns></returns>
        //[HttpPost("wxpay")]
        //public async Task<IActionResult> PayAsync(CreatePayCommand PayCreateDto)
        //{
        //    await _mediator.Send(PayCreateDto);
        //    return Ok();
        //}

    }
}
