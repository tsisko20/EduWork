using EduWork.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppRole>().HasIndex(x => x.Title).IsUnique();

            modelBuilder.Entity<NonWorkingDay>().HasIndex(x => x.NonWorkingDate).IsUnique();

            modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.EntraObjectId).IsUnique();

            modelBuilder.Entity<Project>().HasIndex(x => x.Title).IsUnique();

            modelBuilder.Entity<ProjectRole>().HasIndex(x => x.Title).IsUnique();

            modelBuilder.Entity<UserProjectRole>().HasIndex(x => new { x.User, x.Project, x.ProjectRole }).IsUnique();

            modelBuilder.Entity<WorkDay>().HasIndex(x => new{x.User, x.WorkDate}).IsUnique();

            modelBuilder.Entity<AnnualLeave>().HasIndex(x => new { x.User, x.Year }).IsUnique();
        }
    }
}
