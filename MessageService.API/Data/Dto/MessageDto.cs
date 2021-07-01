using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Data.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public int RelatedId { get; set; }
        public int? FromId { get; set; }
        public int? ToId { get; set; }
        public string IpAddr { get; set; }
        public string MessageContent { get; set; }
        public int ReplyCount { get; set; }
        public bool IsDeleted { get; set; }

    }
}
