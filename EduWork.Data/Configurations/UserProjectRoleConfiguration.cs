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
    internal class UserProjectRoleConfiguration : IEntityTypeConfiguration<UserProjectRole>
    {
        public void Configure(EntityTypeBuilder<UserProjectRole> builder)
        {
            builder
                .HasIndex(x => new { x.UserId, x.ProjectId, x.ProjectRoleId })
                .IsUnique();
        }
    }
}