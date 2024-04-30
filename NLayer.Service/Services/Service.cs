using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositoties;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
        //
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }     
        //
        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity); 
            await _unitOfWork.CommitAsync();
            return entity;
            //throw new NotImplementedException();
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
            //throw new NotImplementedException();
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();

            //throw new NotImplementedException();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);  
            //throw new NotImplementedException();
        }
        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            //throw new NotImplementedException();
        }
        public async Task RemoveRange(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            //throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            
            //throw new NotImplementedException();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
            //throw new NotImplementedException();
        }
    }
}
