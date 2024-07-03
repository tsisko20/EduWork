using EduWork.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EduWork.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(u => u.AppRole)
                .WithMany(ar => ar.Users)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasMany(u => u.UserProjectRoles)
                .WithOne(upr => upr.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasIndex(x => x.Username)
                .IsUnique();

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder
                .HasIndex(x => x.EntraObjectId)
                .IsUnique();
        }
    }
}
