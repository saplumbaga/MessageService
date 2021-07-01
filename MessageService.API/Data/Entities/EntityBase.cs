using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.API.Data.Entities
{
    public class EntityBase
    {
        public EntityBase()
        {
            DateAdded = DateTime.UtcNow;
            IsDeleted = false;
        }
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; }
    }
}
