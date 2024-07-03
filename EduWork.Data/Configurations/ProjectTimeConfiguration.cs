using EduWork.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduWork.Data.Configurations
{
    internal class ProjectTimeConfiguration : IEntityTypeConfiguration<ProjectTime>
    {
        public void Configure(EntityTypeBuilder<ProjectTime> builder)
        {

        }
    }
}
