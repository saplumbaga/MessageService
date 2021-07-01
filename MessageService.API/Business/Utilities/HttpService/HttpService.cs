using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.API.HttpService
{
    public class HttpService : IHttpService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetBaseUrl()
        {
            return $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
        }

        public async Task<string> GetRequestIpAddress()
        {
            return _httpContextAccessor.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString(); ;
        }

        public async Task RemoveCookie(string key)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }

        public async Task SetCookie(string key, string value, int expireDays = 30)
        {
            CookieOptions options = new CookieOptions();

            options.Expires = DateTime.UtcNow.AddDays(expireDays);

            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value ?? string.Empty, options);

        }

        public bool TryGetCookieValue(string cookieKey, out string data)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(cookieKey, out data);
        }
    }
}
