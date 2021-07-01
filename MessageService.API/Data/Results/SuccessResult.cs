using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MessageService.API.Data.Results
{
    public class SuccessResult : Result
    {

        public SuccessResult() : base(true)
        {

        }

        public SuccessResult(IEnumerable<string> Messages) : base(true, Messages)
        {

        }
        public SuccessResult(HttpStatusCode statusCode) : base(true, statusCode)
        {

        }
        public SuccessResult(string message, HttpStatusCode statusCode) : base(true, message, statusCode)
        {
        }

    }
}
