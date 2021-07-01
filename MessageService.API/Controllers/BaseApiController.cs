using MessageService.API.CustomFilters;
using MessageService.API.Data.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Controllers
{
    [ApiController]
    [TokenAuthorize]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected IActionResult ApiResult(IResult result)
        {
            return StatusCode((int)result.StatusCode, result);
        }

    }
}
