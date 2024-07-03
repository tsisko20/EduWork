using EduWork.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduWork.Data.Configurations
{
    internal class SickLeaveRecordConfiguration : IEntityTypeConfiguration<SickLeaveRecord>
    {
        public void Configure(EntityTypeBuilder<SickLeaveRecord> builder)
        {
            builder
                .HasOne(u => u.User)
                .WithMany(slr => slr.SickLeaveRecords)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
