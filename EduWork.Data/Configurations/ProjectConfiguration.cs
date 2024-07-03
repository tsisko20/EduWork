using EduWork.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduWork.Data.Configurations
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .HasMany(p => p.UserProjectRoles)
                .WithOne(upr => upr.Project)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(pr => pr.ProjectTimes)
                .WithOne(pt => pt.Project)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder
               .HasIndex(x => x.Title)
               .IsUnique();

        }
    }
}
