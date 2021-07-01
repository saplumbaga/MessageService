using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.API.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly MessageServiceDBContext _context;

        public UnitOfWork(MessageServiceDBContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
