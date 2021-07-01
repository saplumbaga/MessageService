using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Models
{
    public class MessageQueryParameters : DefaultParams
    {
        public int ToId { get; set; }
        public int FromId { get; set; }
        public int RelatedId { get; set; }
    }
}
