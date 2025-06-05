using Microsoft.EntityFrameworkCore;
using TestTask_aton.Core.Models;
using TestTask_aton.DataAccess.Entities;
using TestTask_aton.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection;
using System.Xml.Linq;

namespace TestTask_aton.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersDBContext _dbContext;

        public async Task<bool> IsUnique(string login)
        {
            var users = await _dbContext.Users
                .Where(u => u.Login == login)
                .AsNoTracking()
                .ToListAsync();

            if (users.Count != 0) return false;

            return true;
        }

        public UsersRepository(UsersDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AdminCheckUp()
        {
            var userEntities = await _dbContext.Users
                .Where(u => u.Login == "Admin")
                .AsNoTracking()
                .ToListAsync();

            if (userEntities.Count == 0)
            {
                var admin = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Login = "Admin",
                    Password = "Admin",
                    Name = "Admin",
                    Gender = 2,
                    BirthDay = DateTime.MinValue,
                    IsAdmin = true,
                    CreatedAt = DateTime.MinValue,
                    CreatedBy = "",
                    ModifiedAt = null,
                    ModifiedBy = "",
                    RevokedAt = null,
                    RevokeddBy = "",

                };

                await _dbContext.AddAsync(admin);
                await _dbContext.SaveChangesAsync();

                return admin.Id;
            }

            return userEntities[0].Id;
        }

        public async Task<List<User>> Get()
        {
            var userEntities = await _dbContext.Users
                .Where(u => u.RevokedAt == null)
                .OrderBy(u => u.CreatedAt)
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

        public async Task<User> GetByLogin(string login)
        {
            var userEntity = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login == login) ?? throw new Exception("Пользователь с таким логином не найден!");

            var user = User.Create(
                    userEntity.Id,
                    userEntity.Login,
                    userEntity.Password,
                    userEntity.Name,
                    userEntity.Gender,
                    userEntity.BirthDay,
                    userEntity.IsAdmin,
                    userEntity.CreatedAt,
                    userEntity.CreatedBy,
                    userEntity.ModifiedAt,
                    userEntity.ModifiedBy,
                    userEntity.RevokedAt,
                    userEntity.RevokeddBy).user;

            return user;
        }

        public async Task<List<User>> GetAllUsersOlderThan(int age)
        {
            var minBirthDate = DateTime.UtcNow.AddYears(-age - 1).Date;

            var userEntities = await _dbContext.Users
                .Where(u => u.BirthDay <= minBirthDate)
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

        public async Task<User> GetYourself(string login, string password)
        {
            var userEntity = await _dbContext.Users
                .Where(u => u.RevokedAt != null)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login == login && u.Password == password) 
                    ?? throw new Exception("Пользлователь с таким логином и паролем не найден!");

            var user = User.Create(
                    userEntity.Id,
                    userEntity.Login,
                    userEntity.Password,
                    userEntity.Name,
                    userEntity.Gender,
                    userEntity.BirthDay,
                    userEntity.IsAdmin,
                    userEntity.CreatedAt,
                    userEntity.CreatedBy,
                    userEntity.ModifiedAt,
                    userEntity.ModifiedBy,
                    userEntity.RevokedAt,
                    userEntity.RevokeddBy).user;

            return user;
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

            if (await IsUnique(user.Login) == false) throw new Exception("Пользователь с таким логином уже существует");

            await _dbContext.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<Guid> UpdateUser(
            Guid id,
            string name,
            int gender,
            DateTime? birthDay,
            DateTime? modifiedAt,
            string modifiedBy)
        {
            await _dbContext.Users
                .Where(u => u.Id == id && u.RevokedAt == null)
                .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Name, u => !string.IsNullOrEmpty(name) ? name : u.Name)
                .SetProperty(u => u.Gender, u => gender != -1 ? gender : u.Gender)
                .SetProperty(u => u.BirthDay, u => birthDay.HasValue ? birthDay : u.BirthDay)
                .SetProperty(u => u.ModifiedAt, modifiedAt)
                .SetProperty(u => u.ModifiedBy, modifiedBy)
                );

            return id;
        }

        public async Task<Guid> UpdateUsersPassword(
            Guid id,
            string password,
            DateTime? modifiedAt,
            string modifiedBy)
        {
            await _dbContext.Users
                .Where(u => u.Id == id && u.RevokedAt == null)
                .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Password, u => !string.IsNullOrEmpty(password) ? password : u.Password)
                .SetProperty(u => u.ModifiedAt, modifiedAt)
                .SetProperty(u => u.ModifiedBy, modifiedBy)
                );

            return id;
        }

        public async Task<Guid> UpdateUsersLogin(
            Guid id,
            string login,
            DateTime? modifiedAt,
            string modifiedBy)
        {
            if (await IsUnique(login) == false) throw new Exception("Пользователь с таким логином уже существует");

            await _dbContext.Users
                .Where(u => u.Id == id && u.RevokedAt == null)
                .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.Login, u => !string.IsNullOrEmpty(login) ? login : u.Login)
                .SetProperty(u => u.ModifiedAt, modifiedAt)
                .SetProperty(u => u.ModifiedBy, modifiedBy)
                );

            return id;
        }

        public async Task<Guid> SoftDelete(
            Guid id,
            DateTime? date,
            string name)
        {
            await _dbContext.Users
                .Where(u => u.Id == id && u.RevokedAt == null)
                .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.RevokedAt, date)
                .SetProperty(u => u.RevokeddBy, name)
                );

            return id;
        }

        public async Task<Guid> UserUpdate2(
            Guid id)
        {
            await _dbContext.Users
                .Where(u => u.Id == id && u.RevokedAt != null)
                .ExecuteUpdateAsync(s => s
                .SetProperty(u => u.RevokedAt, u => null)
                .SetProperty(u => u.RevokeddBy, u => "")
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
