using KonusarakOgren.Core.Repositories;
using KonusarakOgren.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgren.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public Service(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            return entity;
        }

        public async Task<bool> AnyAsnyc(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsnyc(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasProduct = await _repository.GetByIdAsync(id);
            //if (hasProduct == null)
            //{
            //    throw new NotFoundException($"{typeof(T).Name}({id}) not found");
            //}
            return hasProduct;
        }

        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
        }

        public async Task<IQueryable<T>> Where(Expression<Func<T, bool>> expression)
        {
            return await _repository.Where(expression);
        }
    }
}
