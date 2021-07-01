using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Models
{
    public class DefaultParams : PaginationParams
    {
        public int? LastId { get; set; }
        public string Term { get; set; }
        public string Ip { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ShowDeleted { get; set; }
        public string MessageDirection { get; set; }
        public string ReplyDirection { get; set; }

    }
}
