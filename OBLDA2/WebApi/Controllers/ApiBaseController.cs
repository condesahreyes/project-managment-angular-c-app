using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace OBLDA2.Controllers
{

    [ApiController]
    [ServiceFilter(typeof(ExceptionFilter))]
    [EnableCors("AllowAllHeaders")]
    public class ApiBaseController : ControllerBase
    {
    }

}

