using System.Threading.Tasks;
using AutoMapper;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models;

namespace SRS.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UserAccountModel> GetAccountInfoByIdAsync(string id)
        {
            var user = await _repo.GetByIdAsync(id);
            return _mapper.Map<UserAccountModel>(user);
        }

        public async Task<UserInfoModel> GetUserInfoByIdAsync(string id)
        {
            var user = await _repo.GetByIdAsync(id);
            return _mapper.Map<UserInfoModel>(user);
        }

        public async Task<UserInfoModel> UpdateAsync(UserInfoModel user)
        {
            var existingUser = await _repo.GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                return null;
            }

            _mapper.Map(user, existingUser);
            await _repo.UpdateAsync(existingUser);
            return user;
        }
    }
}
