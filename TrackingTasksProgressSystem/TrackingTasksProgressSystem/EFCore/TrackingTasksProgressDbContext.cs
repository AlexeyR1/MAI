using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.EFCore.EntityConfigurations;

namespace TrackingTasksProgressSystem.EFCore
{
    public class TrackingTasksProgressDbContext : DbContext
    {
        public TrackingTasksProgressDbContext(DbContextOptions<TrackingTasksProgressDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        public DbSet<Employee> Employees { get; private set; }
        public DbSet<Department> Departments { get; private set; }
        public DbSet<Position> Positions { get; private set; }
        public DbSet<Models.Task> Tasks { get; private set; }
        public DbSet<Status> Statuses { get; private set; }
        public DbSet<Priority> Priorities { get; private set; }
        public DbSet<Attachment> Attachments { get; private set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new TaskConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            modelBuilder.ApplyConfiguration(new PriorityConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
        }
    }
}
