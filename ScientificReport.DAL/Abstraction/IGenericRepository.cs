﻿using ScientificReport.DAL.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScientificReport.DAL.Abstraction
{
    public interface IGenericRepository<TEntity,T>
    {
        Task<T> CreateAsync(TEntity item);
        Task RemoveAsync(TEntity item);
        Task UpdateAsync(TEntity item);
        Task<TEntity> FindByIdAsync(T id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAsync<TOrder>(QuerySpecification<TEntity,TOrder> specification);
    }
}