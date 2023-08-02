using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Specifications.PublicationSpecifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.PublicationModels;
using SRS.Services.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.Services.Implementations
{
    public class PublicationService : BaseService<Publication>, IPublicationService
    {
        private readonly IRoleActionService _roleActionService;

        public PublicationService(IBaseRepository<Publication> repo, IMapper mapper, IRoleActionService roleActionService)
            : base(repo, mapper)
        {
            _roleActionService = roleActionService;
        }

        public async Task<IList<BasePublicationModel>> GetForUserAsync(UserAccountModel user, PublicationFilterModel filterModel)
        {
            var actions = new Dictionary<string, Func<Task<IList<Publication>>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.GetAsync(new PublicationSpecification(filterModel, null)),
                [RoleNames.RectorateAdmin] = async () => await _repo.GetAsync(new PublicationSpecification(filterModel, null)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.GetAsync(new PublicationSpecification(filterModel, x => x.User.Any(u => u.Id == user.Id || u.Cathedra.FacultyId == user.FacultyId))),
                [RoleNames.CathedraAdmin] = async () => await _repo.GetAsync(new PublicationSpecification(filterModel, x => x.User.Any(u => u.Id == user.Id || u.CathedraId == user.CathedraId))),
                [RoleNames.Worker] = async () => await _repo.GetAsync(new PublicationSpecification(filterModel, x => x.User.Any(u => u.Id == user.Id)))
            };

            var publications = await _roleActionService.TakeRoleActionAsync(user, actions);
            return _mapper.Map<IList<BasePublicationModel>>(publications ?? new List<Publication>());
        }

        public async Task<int> CountForUserAsync(UserAccountModel user, PublicationFilterModel filterModel)
        {
            var countFilterModel = new PublicationFilterModel
            {
                Search = filterModel.Search,
                CathedraId = filterModel.CathedraId,
                FacultyId = filterModel.FacultyId,
                UserId = filterModel.UserId,
                From = filterModel.From,
                To = filterModel.To,
                PublicationType = filterModel.PublicationType
            };

            var actions = new Dictionary<string, Func<Task<int>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.CountAsync(new PublicationSpecification(countFilterModel, null)),
                [RoleNames.RectorateAdmin] = async () => await _repo.CountAsync(new PublicationSpecification(countFilterModel, null)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.CountAsync(new PublicationSpecification(countFilterModel, x => x.User.Any(u => u.Cathedra.FacultyId == user.FacultyId))),
                [RoleNames.CathedraAdmin] = async () => await _repo.CountAsync(new PublicationSpecification(countFilterModel, x => x.User.Any(u => u.CathedraId == user.CathedraId))),
                [RoleNames.Worker] = async () => await _repo.CountAsync(new PublicationSpecification(countFilterModel, x => x.User.Any(u => u.Id == user.Id)))
            };

            return await _roleActionService.TakeRoleActionAsync(user, actions);
        }

        public async Task<IList<BasePublicationModel>> GetAvailableReportPublicationsAsync(ReportPublicationFilterModel filterModel)
        {
            var publications = await _repo.GetAsync(new ReportPublicationSpecification(filterModel));
            return _mapper.Map<IList<BasePublicationModel>>(publications);
        }

        public async Task<IList<BasePublicationModel>> GetAvailableCathedraReportPublicationsAsync(CathedraReportPublicationFilterModel filterModel)
        {
            var publications = await _repo.GetAsync(new CathedraReportPublicationSpecification(filterModel));
            return _mapper.Map<IList<BasePublicationModel>>(publications.Distinct());
        }
    }
}
