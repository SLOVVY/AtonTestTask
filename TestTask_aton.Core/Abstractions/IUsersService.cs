using TestTask_aton.Core.Models;

namespace TestTask_aton.Application.Services
{
    public interface IUsersService
    {
        Task<Guid> CreateUser(User user);
        Task<List<User>> GetAllUsers();
        Task<Guid> HardDelete(Guid id);
        Task<Guid> UpdateUser(Guid id, string login, string password, string name, int gender, DateTime? birthDay, DateTime? modifiedAt, string modifiedBy);
    }
}