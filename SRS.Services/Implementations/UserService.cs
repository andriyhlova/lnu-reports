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
        private readonly Interfaces.IConfigurationProvider _configurationProvider;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository repo,
            IEmailService emailService,
            Interfaces.IConfigurationProvider configurationProvider,
            IMapper mapper)
        {
            _repo = repo;
            _emailService = emailService;
            _configurationProvider = configurationProvider;
            _mapper = mapper;
        }

        public async Task<TUserModel> GetByIdAsync(string id)
        {
            var user = await _repo.GetByIdAsync(id);
            return _mapper.Map<TUserModel>(user);
        }

        public async Task<IList<TUserModel>> GetAsync(UserAccountModel user, UserFilterModel filterModel)
        {
            var users = await TakeRoleAction(
                user,
                async () => await _repo.GetAsync(new UserFilterSpecification(filterModel, null)),
                async () => await _repo.GetAsync(new UserFilterSpecification(filterModel, x => x.Cathedra.FacultyId == user.FacultyId)),
                async () => await _repo.GetAsync(new UserFilterSpecification(filterModel, x => x.Cathedra.Id == user.CathedraId)));

            return _mapper.Map<IList<TUserModel>>(users ?? new List<ApplicationUser>());
        }

        public async Task<int> CountAsync(UserAccountModel user, UserFilterModel filterModel)
        {
            return await TakeRoleAction(
                user,
                async () => await _repo.CountAsync(new UserFilterSpecification(filterModel, null)),
                async () => await _repo.CountAsync(new UserFilterSpecification(filterModel, x => x.Cathedra.FacultyId == user.FacultyId)),
                async () => await _repo.CountAsync(new UserFilterSpecification(filterModel, x => x.Cathedra.Id == user.CathedraId)));
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

        private async Task<TItem> TakeRoleAction<TItem>(UserAccountModel user, Func<Task<TItem>> adminAction, Func<Task<TItem>> deaneryAction, Func<Task<TItem>> cathedraAction)
        {
            if (user.IsInRole(RoleNames.Superadmin) || user.IsInRole(RoleNames.RectorateAdmin))
            {
                return await adminAction();
            }
            else if (user.IsInRole(RoleNames.DeaneryAdmin))
            {
                return await deaneryAction();
            }
            else if (user.IsInRole(RoleNames.CathedraAdmin))
            {
                return await cathedraAction();
            }

            return default;
        }
    }
}
