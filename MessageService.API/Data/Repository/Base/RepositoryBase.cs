using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.API.Data.Repository.Base
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> FindAsNoTrackingAsync(Expression<Func<T, bool>> expression)
        {
            return await _context
                .Set<T>()
                .Where(expression)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>()
                .Where(expression)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>()
                .Where(expression)
                .ToListAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>()
                .Where(expression);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression, Expression<Func<T, bool>> order)
        {
            return _context.Set<T>()
                .Where(expression).OrderBy(order);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                .FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
