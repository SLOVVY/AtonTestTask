using TestTask_aton.Core.Models;

namespace TestTask_aton.Core.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> Create(User user);
        Task<List<User>> Get();
        Task<Guid> HardDelete(Guid id);
        Task<Guid> Update1(Guid id, string login, string password, string name, int gender, DateTime? birthDay, DateTime? modifiedAt, string modifiedBy);
    }
}