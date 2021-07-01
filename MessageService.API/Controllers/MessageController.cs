using MessageService.API.Business.Abstract;
using MessageService.API.Data.Dto;
using MessageService.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MessageService.API.Controllers
{
    public class MessageController : BaseApiController
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }



        [HttpPost]
        public async Task<IActionResult> Index(MessageDto dto)
        {
            return ApiResult(await _messageService.Add(dto));
        }


        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int id)
        {
            return ApiResult(await _messageService.GetDtoById(id));
        }

        [HttpGet]
        [Route("basicinfo")]
        public async Task<IActionResult> BasicInfo([FromQuery] int id)
        {
            return ApiResult(await _messageService.GetBasicInfoDtoById(id));
        }

        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            return ApiResult(await _messageService.Delete(id));
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> List([FromQuery] MessageQueryParameters parameters)
        {
            return ApiResult(await _messageService.List(parameters));
        }

        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> Count([FromQuery] MessageQueryParameters parameters)
        {
            return ApiResult(await _messageService.Count(parameters));
        }

        [HttpGet]
        [Route("topsenders")]
        public async Task<IActionResult> GetTopSenders([FromQuery] DefaultParams parameters)
        {
            return ApiResult(await _messageService.GetTopSendersBetweenDates(parameters));
        }
    }
}
