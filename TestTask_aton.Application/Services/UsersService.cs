using TestTask_aton.Core.Abstractions;
using TestTask_aton.Core.Models;

namespace TestTask_aton.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IJWTProvider _jWTProvider;

        public UsersService(
            IUsersRepository usersRepository,
            IJWTProvider jWTProvider)
        {
            _usersRepository = usersRepository;
            _jWTProvider = jWTProvider;
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
            string name,
            int gender,
            DateTime? birthDay,
            DateTime? modifiedAt,
            string modifiedBy)
        {
            return await _usersRepository.UpdateUser(
                id,
                name,
                gender,
                birthDay,
                modifiedAt,
                modifiedBy);
        }

        public async Task<Guid> UpdateUsersPassword(
            Guid id,
            string password,
            DateTime? modifiedAt,
            string modifiedBy)
        {
            return await _usersRepository.UpdateUsersPassword(
                id,
                password,
                modifiedAt,
                modifiedBy);
        }

        public async Task<Guid> UpdateUsersLogin(
            Guid id,
            string login,
            DateTime? modifiedAt,
            string modifiedBy)
        {
            return await _usersRepository.UpdateUsersLogin(
                id,
                login,
                modifiedAt,
                modifiedBy);
        }

        public async Task<Guid> SoftDelete(
            Guid id,
            DateTime? revokedAt,
            string revokedBy)
        {
            return await _usersRepository.SoftDelete(
                id,
                revokedAt,
                revokedBy);
        }

        public async Task<Guid> Update2(
            Guid id)
        {
            return await _usersRepository.UserUpdate2(
                id
                );
        }

        public async Task<Guid> HardDelete(Guid id)
        {
            return await _usersRepository.HardDelete(id);
        }

        public async Task<User> GetUserByLogin(string login)
        {
            var user = await _usersRepository.GetByLogin(login);

            return user;
        }

        public async Task<User> GetYourself(string login, string password)
        {
            var user = await _usersRepository.GetYourself(login, password);

            return user;
        }

        public async Task<List<User>> GetAllUsersOlderThan(int age)
        {
            return await _usersRepository.GetAllUsersOlderThan(age);
        }

        public async Task<string> Login(string login, string password)
        {
            var user = await GetUserByLogin(login);

            if (password != user.Password) throw new Exception("Пароль неверный, ошибка входа");

            var token = _jWTProvider.GenerateToken(user);

            return token;
        }
    }
}
