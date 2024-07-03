using EduWork.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Data.Configurations
{
    internal class WorkDayConfiguration : IEntityTypeConfiguration<WorkDay>
    {
        public void Configure(EntityTypeBuilder<WorkDay> builder)
        {
            builder
                .HasMany(wd => wd.ProjectTimes)
                .WithOne(pt => pt.WorkDay)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasIndex(x => new { x.UserId, x.WorkDate })
                .IsUnique();
        }
    }
}
