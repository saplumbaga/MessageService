using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Config
{
    public class AppConfig
    {
        public string MessageDirection { get; set; }
        public string ReplyDirection { get; set; }
        public int? MaxReplyCountOnMessageList { get; set; }
    }
}
