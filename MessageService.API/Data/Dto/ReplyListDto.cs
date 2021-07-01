using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Data.Dto
{
    public class ReplyListDto
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public bool IsReply { get; set; }
        public string MessageContent { get; set; }
        public string IpAddr { get; set; }
        public DateTime? DateAdded { get; set; }
        public bool IsDeleted { get; set; }

    }
}
