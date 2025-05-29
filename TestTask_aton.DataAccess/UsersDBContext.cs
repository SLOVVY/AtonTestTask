using Microsoft.EntityFrameworkCore;
using TestTask_aton.DataAccess.Entities;

namespace TestTask_aton.DataAccess
{
    public class UsersDBContext : DbContext
    {
        public UsersDBContext(DbContextOptions<UsersDBContext> options) 
            : base(options)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
