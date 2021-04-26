﻿using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Specifications;
using ScientificReport.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScientificReport.Services.Implementation
{
    public class ServiceBase<TEntity,T> : IServiceBase<TEntity,T> where TEntity : IBaseEntity<T>
    {
        protected readonly IGenericRepository<TEntity,T> repository;

        public ServiceBase(IGenericRepository<TEntity,T> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteAsync(T entityId)
        {
            var found = await repository.FindByIdAsync(entityId);
            if (found == null)
            {
                return false;
            }
            await repository.RemoveAsync(found);
            return true;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var found = await repository.GetAllAsync();
            return found;
        }

        public async Task<IEnumerable<TEntity>> GetAsync<TOrder>(QuerySpecification<TEntity,TOrder> specification)
        {
            var found = await repository.GetAsync(specification);
            return found;
        }

        public async Task<TEntity> GetByIdAsync(T entityId)
        {
            var found = await repository.FindByIdAsync(entityId);
            return found;
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await repository.CreateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            await repository.UpdateAsync(entity);
            return true;
        }
    }
}
