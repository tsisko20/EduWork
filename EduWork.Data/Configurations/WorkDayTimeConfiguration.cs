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
    internal class WorkDayTimeConfiguration : IEntityTypeConfiguration<WorkDayTime>
    {
        public void Configure(EntityTypeBuilder<WorkDayTime> builder)
        { 
            builder
                .HasOne(wdt => wdt.WorkDay)
                .WithMany(wd => wd.WorkDayTimes)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
