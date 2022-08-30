using KonusarakOgren.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgren.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected readonly IdentityAppDbContext _context;
        private readonly DbSet<T> _dbset;

        public GenericRepository(IdentityAppDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
            await SaveChanges();
        }

        public async Task<bool> AnyAsnyc(Expression<Func<T, bool>> expression)
        {
            return await _dbset.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _dbset.AsNoTracking().AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public void Remove(T entity)
        {
            _dbset.Remove(entity);
            Task.FromResult(SaveChanges());
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
            Task.FromResult(SaveChanges());
        }

        public async Task<IQueryable<T>> Where(Expression<Func<T, bool>> expression)
        {
            return _dbset.Where(expression);
        }
        private async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
