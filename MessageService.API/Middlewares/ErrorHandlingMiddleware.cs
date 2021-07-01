using FluentValidation;
using MessageService.API.Data.Results;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = ex.Message;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            if (ex.GetType() == typeof(ValidationException))
            {
                message = "";
                var exception = (ValidationException)ex;
                if (exception != null && exception.Errors.Count() > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    foreach (var error in exception.Errors)
                    {
                        stringBuilder.Append(error.ErrorMessage);
                    }

                    message = stringBuilder.ToString();
                }

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                statusCode = HttpStatusCode.UnprocessableEntity;
            }
            var result = new { StatusCode = statusCode, Message = message };
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
