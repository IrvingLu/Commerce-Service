using Microsoft.AspNetCore.Mvc;

namespace User.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : SfanBaseController
    {

    }
}
