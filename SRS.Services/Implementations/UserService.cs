using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
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

        public virtual async Task<UserModel> GetByUsernameAsync(string username)
        {
            var user = await _repo.GetByUsernameAsync(username);
            return _mapper.Map<UserModel>(user);
        }
    }
}
