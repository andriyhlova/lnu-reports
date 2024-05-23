using AutoMapper;
using SRS.Domain.Entities;
using SRS.Repositories.Interfaces;
using SRS.Services.Extensions;
using SRS.Services.Models.PublicationModels;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SRS.Services.Implementations
{
    public class PublicationCrudService : BaseCrudService<Publication, PublicationModel>
    {
        public PublicationCrudService(IBaseRepository<Publication> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public override async Task<int> AddAsync(PublicationModel model)
        {
            var normalizedName = model.Name.NormalizeText();
            var existingPublication = await _repo
                .GetFirstOrDefaultAsync(GetPublicationExistsExpression(model, normalizedName));
            if (existingPublication == null)
            {
                return await base.AddAsync(model);
            }

            return 0;
        }

        public override async Task<PublicationModel> UpdateAsync(PublicationModel model)
        {
            var normalizedName = model.Name.NormalizeText();
            var existingPublication = await _repo.GetFirstOrDefaultAsync(GetPublicationExistsExpression(model, normalizedName));
            if (existingPublication == null)
            {
                return await base.UpdateAsync(model);
            }

            return null;
        }

        private Expression<Func<Publication, bool>> GetPublicationExistsExpression(PublicationModel model, string normalizedName)
        {
            return x => x.Id != model.Id &&
                   x.PublicationType == model.PublicationType &&
                   x.Name.Trim().ToUpper().Replace("  ", " \u0001").Replace("\u0001 ", string.Empty).Replace("\u0001", string.Empty) == normalizedName;
        }
    }
}
