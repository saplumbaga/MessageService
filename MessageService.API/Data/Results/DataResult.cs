using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MessageService.API.Data.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success, string message, HttpStatusCode statusCode) : base(success, message, statusCode)
        {
            Data = data;
        }
        public DataResult(T data, bool success, HttpStatusCode statusCode) : base(success, statusCode)
        {
            Data = data;
        }
        public DataResult(T data, bool success, HttpStatusCode statusCode, int totalCount) : base(success, statusCode)
        {
            TotalCount = totalCount;
            Data = data;

        }
        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public DataResult(T data, bool success, HttpStatusCode statusCode,Dictionary<string,string> modelStateERrors) : base(statusCode,modelStateERrors)
        {
            Data = data;
        }
        public T Data { get; }
        public int TotalCount { get; }

    }
}
