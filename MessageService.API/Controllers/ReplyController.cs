using MessageService.API.Business.Abstract;
using MessageService.API.Data.Dto;
using MessageService.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Controllers
{
    public class ReplyController : BaseApiController
    {
        private readonly IReplyService _replyService;
        public ReplyController(IReplyService replyService)
        {
            _replyService = replyService;
        }


        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] DefaultParams defaultParams, [FromQuery]int messageId)
        {
            return ApiResult(await _replyService.List(defaultParams, messageId));
        }


        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            return ApiResult(await _replyService.Delete(id));
        }


        [HttpPost]
        public async Task<IActionResult> Index(ReplyDto dto)
        {
            return ApiResult(await _replyService.Add(dto));
        }
    }
}
