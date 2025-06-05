using TestTask_aton.Core.Models;

namespace TestTask_aton.Core.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> AdminCheckUp();
        Task<Guid> Create(User user);
        Task<List<User>> Get();
        Task<List<User>> GetAllUsersOlderThan(int age);
        Task<User> GetByLogin(string login);
        Task<User> GetYourself(string login, string password);
        Task<Guid> HardDelete(Guid id);
        Task<Guid> SoftDelete(Guid id, DateTime? date, string name);
        Task<Guid> UpdateUser(Guid id, string name, int gender, DateTime? birthDay, DateTime? modifiedAt, string modifiedBy);
        Task<Guid> UpdateUsersLogin(Guid id, string login, DateTime? modifiedAt, string modifiedBy);
        Task<Guid> UpdateUsersPassword(Guid id, string password, DateTime? modifiedAt, string modifiedBy);
        Task<Guid> UserUpdate2(Guid id);
    }
}