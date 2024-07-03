using EduWork.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AnnualLeave> AnnualLeaves { get; set; } = null!;
        public DbSet<AnnualLeaveRecord> AnnualLeaveRecords { get; set; } = null!;
        public DbSet<AppRole> AppRoles { get; set; } = null!;
        public DbSet<NonWorkingDay> NonWorkingDays { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<ProjectRole> ProjectRoles { get; set; } = null!;
        public DbSet<ProjectTime> ProjectTimeRecords { get; set; } = null!;
        public DbSet<SickLeaveRecord> SickLeaveRecords { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserProjectRole> UserProjectRoles { get; set; } = null!;
        public DbSet<WorkDay> WorkDays { get; set; } = null!;
        public DbSet<WorkDayTime> WorkDayTimeRecords { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=.;Database=FactoryApp;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
