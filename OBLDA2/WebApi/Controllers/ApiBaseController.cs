using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace OBLDA2.Controllers
{

    [ApiController]
    [ServiceFilter(typeof(ExceptionFilter))]
    public class ApiBaseController : ControllerBase
    {
    }

}

