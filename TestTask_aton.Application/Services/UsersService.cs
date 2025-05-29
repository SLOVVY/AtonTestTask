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
            bool isAdmin,
            DateTime createdAt,
            string createdBy,
            DateTime modifiedAt,
            string modifiedBy,
            DateTime revokedAt,
            string revokedBy)
        {
            return await _usersRepository.Update(
                id,
                login,
                password,
                name,
                gender,
                birthDay,
                isAdmin,
                createdAt,
                createdBy,
                modifiedAt,
                modifiedBy,
                revokedAt,
                revokedBy);
        }

        public async Task<Guid> HardDeleteBook(Guid id)
        {
            return await _usersRepository.HardDelete(id);
        }
    }
}
