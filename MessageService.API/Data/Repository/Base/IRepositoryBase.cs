using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.API.Data.Repository.Base
{
    public interface IRepositoryBase<T> where T : class, new()
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<T> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> FindAsNoTrackingAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        IQueryable<T> Where(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> order);
        Task<T> GetByIdAsync(int id);
    }
}
