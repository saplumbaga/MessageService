using MessageService.API.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Data.Entities
{
    public class Log : EntityBase
    {
        public int MessageId { get; set; }
        public Message Message { get; set; }
        public ActionTypes Action { get; set; }
    }
}
