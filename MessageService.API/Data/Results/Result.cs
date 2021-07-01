using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MessageService.API.Data.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message, HttpStatusCode statusCode) : this(success)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public Result(bool success, string message, HttpStatusCode statusCode, Type exceptionType) : this(success)
        {
            Message = message;
            StatusCode = statusCode;
            ExceptionType = exceptionType;
        }

        public Result(bool succes, HttpStatusCode statusCode, IEnumerable<string> errors)
        {
            Success = succes;
            StatusCode = statusCode;
            Messages = errors;
        }
        public Result(bool succes, HttpStatusCode statusCode)
        {
            Success = succes;
            StatusCode = statusCode;
        }

        public Result(HttpStatusCode statusCode, Dictionary<string, string> modelStateErrors)
        {
            StatusCode = statusCode;
            ModelStateErrors = modelStateErrors;
        }

        public Result(Type exceptionType, Dictionary<string, string> modelStateErrors)
        {
            ModelStateErrors = modelStateErrors;
            ExceptionType = exceptionType;
        }
        public Result(bool succes)
        {
            Success = succes;
        }

        public Result(bool succes, IEnumerable<string> messages)
        {
            Success = succes;
            Messages = messages;
        }
        public bool Success { get; }
        public string Message { get; }
        public Type ExceptionType { get; }
        public HttpStatusCode StatusCode { get; }
        public IEnumerable<string> Messages { get; set; }

        public Dictionary<string, string> ModelStateErrors { get; }
    }
}
