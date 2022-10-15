using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Specifications.PublicationSpecifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Extensions;
using SRS.Services.Interfaces;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.PublicationModels;
using SRS.Services.Models.UserModels;

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
                .GetFirstOrDefaultAsync(x => x.Name.Trim().ToUpper().Replace("  ", " \u0001").Replace("\u0001 ", string.Empty).Replace("\u0001", string.Empty) == normalizedName);
            if (existingPublication == null)
            {
                return await base.AddAsync(model);
            }

            return 0;
        }

        public override async Task<PublicationModel> UpdateAsync(PublicationModel model)
        {
            var normalizedName = model.Name.NormalizeText();
            var existingPublication = await _repo.GetFirstOrDefaultAsync(x => x.Id != model.Id && x.Name.Trim().ToUpper().Replace("  ", " \u0001").Replace("\u0001 ", string.Empty).Replace("\u0001", string.Empty) == normalizedName);
            if (existingPublication == null)
            {
                return await base.UpdateAsync(model);
            }

            return null;
        }
    }
}
