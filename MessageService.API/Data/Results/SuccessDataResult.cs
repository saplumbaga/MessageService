using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MessageService.API.Data.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data) : base(data, true, HttpStatusCode.OK)
        {

        }

        public SuccessDataResult(T data, int dataCount) : base(data, true, HttpStatusCode.OK,dataCount)
        {

        }
        public SuccessDataResult(T data, HttpStatusCode statusCode) : base(data, true, statusCode)
        {

        }

        public SuccessDataResult(T data, HttpStatusCode statusCode, int dataCount) : base(data, true, statusCode,dataCount)
        {

        }
        public SuccessDataResult(T data, string message, HttpStatusCode statusCode) : base(data, true, message, statusCode)
        {

        }
        public SuccessDataResult(string message, HttpStatusCode statusCode) : base(default, true, message, statusCode)
        {

        }
        public SuccessDataResult() : base(default, true)
        {

        }
    }
}
