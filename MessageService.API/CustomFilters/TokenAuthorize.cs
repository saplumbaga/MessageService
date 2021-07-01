using MessageService.API.Config;
using MessageService.API.HttpService;
using MessageService.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MessageService.API.CustomFilters
{
    public class TokenAuthorize : ActionFilterAttribute
    {
        public string Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _securityConfig = (SecurityConfig)context.HttpContext.RequestServices.GetService(typeof(SecurityConfig));
            var _httpService = (IHttpService)context.HttpContext.RequestServices.GetService(typeof(IHttpService));

            var token = context.HttpContext.Request.Headers["Token"].ToString();

            var requestIp = _httpService.GetRequestIpAddress().Result;

            if (!IsAuthenticationEnabled(_securityConfig))
            {
                return;
            }

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
            }

            if (_securityConfig.IpAddresses?.Count > 0 && !_securityConfig.IpAddresses.Contains(requestIp))
            {
                context.Result = new UnauthorizedResult();
            }

            if (_securityConfig.Keys?.Count > 0 && !_securityConfig.Keys.Contains(token))
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private bool IsAuthenticationEnabled(SecurityConfig config)
        {
            return !((config.Keys == null && config.IpAddresses == null) || (config.Keys?.Count == 0 && config.IpAddresses?.Count == 0));
        }
    }
}
