using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.API.Data.Results
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
        public int TotalCount { get; }
    }
}
