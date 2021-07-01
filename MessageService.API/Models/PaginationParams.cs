using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Models
{
    public class PaginationParams
    {
        public int Take { get; set; }
        public int Skip { get; set; }

    }
}
