using MessageService.API.Data.Dto;
using MessageService.API.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Business.Abstract
{
    public interface ILogService
    {
        Task<IDataResult<LogDto>> Add(LogDto dto);
    }
}
