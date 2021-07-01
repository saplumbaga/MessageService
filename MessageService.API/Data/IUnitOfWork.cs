using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.API.Data
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
