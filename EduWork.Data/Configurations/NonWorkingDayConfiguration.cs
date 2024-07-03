using EduWork.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduWork.Data.Configurations
{
    internal class NonWorkingDayConfiguration : IEntityTypeConfiguration<NonWorkingDay>
    {
        public void Configure(EntityTypeBuilder<NonWorkingDay> builder)
        {
            builder
                .HasIndex(x => x.NonWorkingDate)
                .IsUnique();
        }
    }
}
