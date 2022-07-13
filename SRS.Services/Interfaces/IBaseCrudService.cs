﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models;

namespace SRS.Services.Interfaces
{
    public interface IBaseCrudService<TModel>
        where TModel : BaseModel
    {
        Task<TModel> GetAsync(int id);

        Task<IList<TModel>> GetAllAsync();

        Task<IList<TModel>> GetAllAsync(int? skip, int? take);

        Task<int> AddAsync(TModel model);

        Task<TModel> UpdateAsync(TModel model);

        Task<bool> DeleteAsync(int id);

        Task<int> CountAsync();
    }
}
