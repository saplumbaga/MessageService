using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Data.Dto
{
    public class MessageTopSenderDto
    {
        public int FromId { get; set; }
        public int Count { get; set; }
    }
}
