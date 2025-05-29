using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask_aton.DataAccess.Entities;

namespace TestTask_aton.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Login)
                .IsRequired();

            builder.Property(u => u.Password)
                .IsRequired();

            builder.Property(u => u.Name)
                .IsRequired();

            builder.Property(u => u.Gender)
                .IsRequired();

            builder.Property(u => u.BirthDay)
                .IsRequired();

            builder.Property(u => u.IsAdmin)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.CreatedBy)
                .IsRequired();

            builder.Property(u => u.ModifiedAt)
                .IsRequired();

            builder.Property(u => u.ModifiedBy)
                .IsRequired();

            builder.Property(u => u.RevokedAt)
                .IsRequired();

            builder.Property(u => u.RevokeddBy)
                .IsRequired();
        }
    }
}
