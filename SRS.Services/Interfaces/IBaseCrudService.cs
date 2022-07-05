using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SRS.Services.Models;

namespace SRS.Services.Interfaces
{
    public interface IBaseCrudService<TModel>
        where TModel : BaseModel
    {
        Task<TModel> Get(int id);

        Task<IList<TModel>> GetAll();

        Task<int> Add(TModel model);

        Task<TModel> Update(TModel model);

        Task<bool> Delete(int id);
    }
}
