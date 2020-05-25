using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Web.Application.Command;
using Shared.Infrastructure.Core.Dapper;
using System.Threading.Tasks;

namespace Order.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IDapperQuery<Core.Domain.Order> _orderQueries;
        public OrderController(IMediator mediator, IDapperQuery<Core.Domain.Order> orderQueries)
        {
            _mediator = mediator;
            _orderQueries = orderQueries;
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetResultAsync()
        {
            var ss = await _orderQueries.QueryAsync("SELECT * FROM `Order`");
            return Ok(ss);
        }
        [HttpPost("insert")]
        public async Task<IActionResult> InsertAsync(CreateCommand  command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
