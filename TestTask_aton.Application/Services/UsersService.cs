using TestTask_aton.Core.Abstractions;
using TestTask_aton.Core.Models;

namespace TestTask_aton.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersRepository.Get();
        }

        public async Task<Guid> CreateUser(User user)
        {
            return await _usersRepository.Create(user);
        }

        public async Task<Guid> UpdateUser(
            Guid id,
            string login,
            string password,
            string name,
            int gender,
            DateTime? birthDay,
            DateTime? modifiedAt,
            string modifiedBy)
        {
            return await _usersRepository.Update1(
                id,
                login,
                password,
                name,
                gender,
                birthDay,
                modifiedAt,
                modifiedBy);
        }

        public async Task<Guid> HardDelete(Guid id)
        {
            return await _usersRepository.HardDelete(id);
        }
    }
}
