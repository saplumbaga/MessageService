using MessageService.API.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Data.Dto
{
    public class LogDto
    {
        public int MessageId { get; set; }
        public ActionTypes Action { get; set; }
        public int UserId { get; set; }
    }
}
