using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.API.Business.Utilities
{
    public static class GuidHelper
    {
        public static string Generate()
        {
            var guid = DateTime.UtcNow.ToString("yyMMddHHmmsss") + "-" + Guid.NewGuid();
            return guid;
        }
    }
}
