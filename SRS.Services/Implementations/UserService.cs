using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;

namespace SRS.Services.Implementations
{
    public class UserService<TUserModel> : IUserService<TUserModel>
        where TUserModel : BaseUserModel
    {
        private readonly IUserRepository _repo;
        private readonly IEmailService _emailService;
        private readonly IRoleActionService _roleActionService;
        private readonly Interfaces.IConfigurationProvider _configurationProvider;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository repo,
            IEmailService emailService,
            IRoleActionService roleActionService,
            Interfaces.IConfigurationProvider configurationProvider,
            IMapper mapper)
        {
            _repo = repo;
            _emailService = emailService;
            _roleActionService = roleActionService;
            _configurationProvider = configurationProvider;
            _mapper = mapper;
        }

        public async Task<TUserModel> GetByIdAsync(string id)
        {
            var user = await _repo.GetByIdAsync(id);
            return _mapper.Map<TUserModel>(user);
        }

        public async Task<IList<TUserModel>> GetForUserAsync(UserAccountModel user)
        {
            var actions = new Dictionary<string, Func<Task<IList<ApplicationUser>>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.GetAllAsync(),
                [RoleNames.RectorateAdmin] = async () => await _repo.GetAllAsync(),
                [RoleNames.DeaneryAdmin] = async () => await _repo.GetAsync(new UserWithInitialsSpecification(x => x.Cathedra.FacultyId == user.FacultyId)),
                [RoleNames.CathedraAdmin] = async () => await _repo.GetAsync(new UserWithInitialsSpecification(x => x.Cathedra.Id == user.CathedraId))
            };

            var users = await _roleActionService.TakeRoleActionAsync(user, actions);
            return _mapper.Map<IList<TUserModel>>(users ?? new List<ApplicationUser>());
        }

        public async Task<IList<TUserModel>> GetAsync(UserAccountModel user, DepartmentFilterModel filterModel)
        {
            var actions = new Dictionary<string, Func<Task<IList<ApplicationUser>>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.GetAsync(new UserFilterSpecification(filterModel, null)),
                [RoleNames.RectorateAdmin] = async () => await _repo.GetAsync(new UserFilterSpecification(filterModel, null)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.GetAsync(new UserFilterSpecification(filterModel, x => x.Cathedra.FacultyId == user.FacultyId)),
                [RoleNames.CathedraAdmin] = async () => await _repo.GetAsync(new UserFilterSpecification(filterModel, x => x.Cathedra.Id == user.CathedraId))
            };

            var users = await _roleActionService.TakeRoleActionAsync(user, actions);
            return _mapper.Map<IList<TUserModel>>(users ?? new List<ApplicationUser>());
        }

        public async Task<int> CountAsync(UserAccountModel user, DepartmentFilterModel filterModel)
        {
            var countFilterModel = new DepartmentFilterModel
            {
                Search = filterModel.Search,
                CathedraId = filterModel.CathedraId,
                FacultyId = filterModel.FacultyId
            };

            var actions = new Dictionary<string, Func<Task<int>>>
            {
                [RoleNames.Superadmin] = async () => await _repo.CountAsync(new UserFilterSpecification(countFilterModel, null)),
                [RoleNames.RectorateAdmin] = async () => await _repo.CountAsync(new UserFilterSpecification(countFilterModel, null)),
                [RoleNames.DeaneryAdmin] = async () => await _repo.CountAsync(new UserFilterSpecification(countFilterModel, x => x.Cathedra.FacultyId == user.FacultyId)),
                [RoleNames.CathedraAdmin] = async () => await _repo.CountAsync(new UserFilterSpecification(countFilterModel, x => x.Cathedra.Id == user.CathedraId))
            };

            return await _roleActionService.TakeRoleActionAsync(user, actions);
        }

        public async Task<TUserModel> UpdateAsync(TUserModel user, string approvedById = null)
        {
            var existingUser = await _repo.GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                return null;
            }

            var isApproved = !existingUser.IsActive && user.IsActive;
            existingUser.ApprovedById = isApproved ? approvedById : existingUser.ApprovedById;

            _mapper.Map(user, existingUser);
            await _repo.UpdateAsync(existingUser);

            if (isApproved)
            {
                await _emailService.SendEmail(
                    existingUser.Email,
                    "Підтвердження користувача",
                    $"Ваш профіль підтверджено в системі звітування <a href=\"{_configurationProvider.Get("WebsiteUrl")}\">{_configurationProvider.Get("WebsiteUrl")}</a>.");
            }

            return user;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
