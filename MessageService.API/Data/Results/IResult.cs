using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MessageService.API.Data.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
        HttpStatusCode StatusCode { get; }
        Dictionary<string,string> ModelStateErrors { get; }
        IEnumerable<string> Messages { get; set; }
        public Type ExceptionType { get; }
    }
}
