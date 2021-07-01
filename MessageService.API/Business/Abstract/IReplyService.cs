using MessageService.API.Data.Dto;
using MessageService.API.Data.Results;
using MessageService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Business.Abstract
{
    public interface IReplyService
    {
        Task<IDataResult<List<ReplyListDto>>> List(DefaultParams parameters, int messageId);
        Task<IDataResult<ReplyDto>> Add(ReplyDto dto);
        Task<IResult> Delete(int id);
    }
}
