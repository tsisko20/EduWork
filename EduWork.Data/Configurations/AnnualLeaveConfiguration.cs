using EduWork.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduWork.Data.Configurations
{
    internal class AnnualLeaveConfiguration : IEntityTypeConfiguration<AnnualLeave>
    {
        public void Configure(EntityTypeBuilder<AnnualLeave> builder) 
        { 
            builder
                .HasOne(al => al.User)
                .WithMany(u => u.AnnualLeaves)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasIndex(x => new { x.UserId, x.Year })
                .IsUnique();
        }
    }
}
