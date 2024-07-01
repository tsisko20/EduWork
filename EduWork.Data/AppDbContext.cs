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
            //optionsBuilder.UseSqlServer("Server=.;Database=FactoryApp;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cascading
            modelBuilder.Entity<User>()
                .HasOne(u => u.AppRole)
                .WithMany(ar => ar.Users)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<AnnualLeaveRecord>()
                .HasOne(u => u.User)
                .WithMany(alr => alr.AnnualLeaveRecords)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SickLeaveRecord>()
                .HasOne(u => u.User)
                .WithMany(slr => slr.SickLeaveRecords)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserProjectRoles)
                .WithOne(upr => upr.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.UserProjectRoles)
                .WithOne(upr => upr.Project)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectRole>()
                .HasMany(pr => pr.UserProjectRoles)
                .WithOne(upr => upr.ProjectRole)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Project>()
                .HasMany(pr => pr.ProjectTimes)
                .WithOne(pt => pt.Project)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkDay>()
                .HasMany(wd => wd.ProjectTimes)
                .WithOne(pt => pt.WorkDay)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkDayTime>()
                .HasOne(wdt => wdt.WorkDay)
                .WithMany(wd => wd.WorkDayTimes)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AnnualLeave>()
                .HasOne(al => al.User)
                .WithMany(u => u.AnnualLeaves)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<WorkDay>()
                .HasOne(wd => wd.User)
                .WithMany(u => u.WorkDays)
                .OnDelete(DeleteBehavior.Cascade);
            
            //Unique key
            modelBuilder.Entity<AppRole>().HasIndex(x => x.Title).IsUnique();

            modelBuilder.Entity<NonWorkingDay>().HasIndex(x => x.NonWorkingDate).IsUnique();

            modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(x => x.EntraObjectId).IsUnique();

            modelBuilder.Entity<Project>().HasIndex(x => x.Title).IsUnique();

            modelBuilder.Entity<ProjectRole>().HasIndex(x => x.Title).IsUnique();

            modelBuilder.Entity<UserProjectRole>().HasIndex(x => new { x.UserId, x.ProjectId, x.ProjectRoleId }).IsUnique();

            modelBuilder.Entity<WorkDay>().HasIndex(x => new{x.UserId, x.WorkDate}).IsUnique();

            modelBuilder.Entity<AnnualLeave>().HasIndex(x => new { x.UserId, x.Year }).IsUnique();
            
            
        }
    }
}
