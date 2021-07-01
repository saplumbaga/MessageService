using MessageService.API.Data.Dto;
using MessageService.API.Data.Results;
using MessageService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Business.Abstract
{
    public interface IMessageService
    {
        Task<IDataResult<List<MessageListDto>>> List(MessageQueryParameters parameters);
        Task<IDataResult<MessageDto>> Add(MessageDto dto);
        Task<IDataResult<MessageDto>> GetDtoById(int id);
        Task<IDataResult<MessageBasicInfoDto>> GetBasicInfoDtoById(int id);
        Task<IDataResult<int>> Count(MessageQueryParameters parameters);
        Task<IResult> Delete(int id);

        Task<IDataResult<List<MessageTopSenderDto>>> GetTopSendersBetweenDates(DefaultParams parameters);
    }
}
