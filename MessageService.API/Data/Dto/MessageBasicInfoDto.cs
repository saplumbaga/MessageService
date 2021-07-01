using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Data.Dto
{
    public class MessageBasicInfoDto
    {
        public int RelatedId { get; set; }
        public int? FromId { get; set; }
        public int? ToId { get; set; }
    }
}
