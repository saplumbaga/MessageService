using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MessageService.API.Data.Results
{
    public class ErrorResult : Result
    {

       

        public ErrorResult() : base(false)
        {

        }

        public ErrorResult(string message, Type exceptionType) : base(false, message, HttpStatusCode.BadRequest,exceptionType)
        {
        }
        public ErrorResult(string message) : base(false, message, HttpStatusCode.BadRequest)
        {
        }
        public ErrorResult(string message, HttpStatusCode statusCode) : base(false, message, statusCode)
        {
        }

        public ErrorResult(HttpStatusCode statusCode, IEnumerable<string> Errors) : base(false, statusCode,Errors)
        {
        }
        public ErrorResult(HttpStatusCode statusCode, Dictionary<string,string> Errors) : base(statusCode,Errors)
        {
        }

        public ErrorResult(Type exceptionType, Dictionary<string, string> Errors) : base(exceptionType, Errors)
        {
        }


    }
}
