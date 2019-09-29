using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProjectHandler.API.Models;

namespace ProjectHandler.API.DatabaseLayer
{
    public class TaskHandlerDbContext : DbContext
    {
        public TaskHandlerDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<TaskItem> Tasks { get; set; }

        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Project> Project { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = DOTNET; Database = TaskDB; Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TaskItem>().HasKey("Id");
            builder.Entity<TaskItem>().ToTable("Task");
            builder.Entity<TaskItem>().Property(p => p.Name).HasColumnName("Task").HasMaxLength(100).IsRequired();
            builder.Entity<TaskItem>().Property(p => p.ParentTaskId).HasColumnName("ParentId");
            builder.Entity<TaskItem>().Property(p => p.StartDate).HasColumnName("Start_Date").IsRequired();
            builder.Entity<TaskItem>().Property(p => p.EndDate).HasColumnName("End_Date").IsRequired();
            builder.Entity<TaskItem>().Property(p => p.Priority).IsRequired();
            builder.Entity<TaskItem>().Property(p => p.ParentTaskId).HasColumnName("ParentId");
            builder.Entity<TaskItem>().Property(p => p.ProjectId).HasColumnName("ProjectId");

            builder.Entity<Project>().HasKey("Id");
            builder.Entity<Project>().ToTable("Project");
            builder.Entity<Project>().Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Entity<Project>().Property(p => p.StartDate).IsRequired();
            builder.Entity<Project>().Property(p => p.EndDate).IsRequired();
            builder.Entity<Project>().Property(p => p.Priority).IsRequired();

            builder.Entity<User>().HasKey("Id");
            builder.Entity<User>().ToTable("User");
            builder.Entity<User>().Property(p => p.FirstName).HasMaxLength(100).IsRequired();
            builder.Entity<User>().Property(p => p.LastName).HasMaxLength(100).IsRequired();
            builder.Entity<User>().Property(p => p.EmployeeId).IsRequired();
            builder.Entity<User>().Property(p => p.ProjectId);
            builder.Entity<User>().Property(p => p.TaskId);
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TaskHandlerDbContext>
    {
        public TaskHandlerDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TaskHandlerDbContext>();
            builder.UseSqlServer(@"Server = DOTNET; Database = TaskDB; Trusted_Connection = True;");

            return new TaskHandlerDbContext(builder.Options);
        }
    }
}