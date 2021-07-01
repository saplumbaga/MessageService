using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Config
{
    public class SecurityConfig
    {
        public List<string> Keys { get; set; }
        public List<string> IpAddresses { get; set; }
    }
}
