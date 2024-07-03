using EduWork.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduWork.Data.Configurations 
{
    internal class AnnualLeaveRecordConfiguration : IEntityTypeConfiguration<AnnualLeaveRecord>
    {
        public void Configure(EntityTypeBuilder<AnnualLeaveRecord> builder) { 
        
            builder
                .HasOne(u => u.User)
                .WithMany(alr => alr.AnnualLeaveRecords)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
