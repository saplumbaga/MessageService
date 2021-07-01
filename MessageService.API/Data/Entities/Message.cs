using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Data.Entities
{
    public class Message : EntityBase
    {
        public int RelatedId { get; set; }
        public int? FromId { get; set; }
        public int? ToId { get; set; }
        public string IpAddr { get; set; }
        public string MessageContent { get; set; }
        public ICollection<Reply> Replies { get; set; }
        public ICollection<Log> Logs { get; set; }
    }
}
