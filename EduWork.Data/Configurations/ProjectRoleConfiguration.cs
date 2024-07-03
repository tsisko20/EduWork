using EduWork.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduWork.Data.Configurations
{
    internal class ProjectRoleConfiguration : IEntityTypeConfiguration<ProjectRole>
    {
        public void Configure(EntityTypeBuilder<ProjectRole> builder)
        {
            builder
                .HasMany(pr => pr.UserProjectRoles)
                .WithOne(upr => upr.ProjectRole)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasIndex(x => x.Title)
                .IsUnique();
        }
    }
}
