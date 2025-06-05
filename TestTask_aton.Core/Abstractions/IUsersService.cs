using Microsoft.AspNetCore.Http;
using TestTask_aton.Core.Models;

namespace TestTask_aton.Core.Abstractions
{
    public interface IUsersService
    {
        Task<Guid> CreateUser(User user);
        Task<List<User>> GetAllUsers();
        Task<List<User>> GetAllUsersOlderThan(int age);
        Task<User> GetUserByLogin(string login);
        Task<User> GetYourself(string login, string password);
        Task<Guid> HardDelete(Guid id);
        Task<string> Login(string login, string password);
        Task<Guid> SoftDelete(Guid id, DateTime? revokedAt, string revokedBy);
        Task<Guid> Update2(Guid id);
        Task<Guid> UpdateUser(Guid id, string name, int gender, DateTime? birthDay, DateTime? modifiedAt, string modifiedBy);
        Task<Guid> UpdateUsersLogin(Guid id, string login, DateTime? modifiedAt, string modifiedBy);
        Task<Guid> UpdateUsersPassword(Guid id, string password, DateTime? modifiedAt, string modifiedBy);
    }
}