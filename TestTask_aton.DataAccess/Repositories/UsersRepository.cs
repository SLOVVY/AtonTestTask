using Microsoft.EntityFrameworkCore;
using TestTask_aton.Core.Models;
using TestTask_aton.DataAccess.Entities;
using TestTask_aton.Core.Abstractions;

namespace TestTask_aton.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersDBContext _dbContext;

        public UsersRepository(UsersDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> Get()
        {
            var userEntities = await _dbContext.Users
                .AsNoTracking()
                .ToListAsync();

            var users = userEntities
                .Select(u => User.Create(
                    u.Id,
                    u.Login,
                    u.Password,
                    u.Name,
                    u.Gender,
                    u.BirthDay,
                    u.IsAdmin,
                    u.CreatedAt,
                    u.CreatedBy,
                    u.ModifiedAt,
                    u.ModifiedBy,
                    u.RevokedAt,
                    u.RevokeddBy).user)
                    .ToList();

            return users;
        }

        public async Task<Guid> Create(User user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Name = user.Name,
                Gender = user.Gender,
                BirthDay = user.BirthDay,
                IsAdmin = user.IsAdmin,
                CreatedAt = user.CreatedAt,
                CreatedBy = user.CreatedBy,
                ModifiedAt = user.ModifiedAt,
                ModifiedBy = user.ModifiedBy,
                RevokedAt = user.RevokedAt,
                RevokeddBy = user.RevokeddBy,
            };

            await _dbContext.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<Guid> Update1(
            Guid id,
            string login,
            string password,
            string name,
            int gender,
            DateTime? birthDay,
            DateTime? modifiedAt,
            string modifiedBy)
        {
            await _dbContext.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Login, login)
                .SetProperty(u => u.Password, password)
                .SetProperty(u => u.Name, name)
                .SetProperty(u => u.Gender, gender)
                .SetProperty(u => u.BirthDay, birthDay)
                .SetProperty(u => u.ModifiedAt, modifiedAt)
                .SetProperty(u => u.ModifiedBy, modifiedBy)
                );

            return id;
        }

        public async Task<Guid> HardDelete(Guid id)
        {
            await _dbContext.Users
                .Where(u => u.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
