using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Data.Entities
{
    public class Reply: EntityBase
    {
        public int MessageId { get; set; }
        public Message Message { get; set; }
        public bool IsReply { get; set; }
        public string MessageContent { get; set; }
        public string  IpAddr{ get; set; }

    }
}
