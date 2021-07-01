using MessageService.API.Business.Abstract;
using MessageService.API.Data.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Controllers
{
    public class LogController : BaseApiController
    {
        private readonly ILogService _logService;
        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public async Task<IActionResult> Index(LogDto logDto)
        {
            return ApiResult(await _logService.Add(logDto));
        }
    }
}
