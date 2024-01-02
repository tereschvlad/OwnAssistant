using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace OwnAssistantCommon.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option) 
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<CustomerTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Table Users
            modelBuilder.Entity<User>().HasOne<Role>(x => x.Role).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<User>().HasMany<CustomerTask>(x => x.CreatedTasks).WithOne(x => x.CreatorUser).HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany<CustomerTask>(x => x.PerformingTasks).WithMany(x => x.PerformingUsers);
            modelBuilder.Entity<User>().Property(x => x.CrtDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            //Table Tasks
            modelBuilder.Entity<CustomerTask>().HasOne<User>(x => x.CreatorUser).WithMany(x => x.CreatedTasks).HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CustomerTask>().HasMany<User>(x => x.PerformingUsers).WithMany(x => x.PerformingTasks);
            modelBuilder.Entity<CustomerTask>().Property(x => x.CrtDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }

    /// <summary>
    /// Db model for project user
    /// </summary>
    [Table("Users")]
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime CrtDate { get; set; }

        public Guid? RoleId { get; set; }

        public Role Role { get; set; }

        public List<CustomerTask> CreatedTasks { get; set; }

        public List<CustomerTask> PerformingTasks { get; set; }
    }

    /// <summary>
    /// Db model for role by user
    /// </summary>
    [Table("Roles")]
    public class Role
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    /// <summary>
    /// Db model for tasks
    /// </summary>
    [Table("Tasks")]
    public class CustomerTask
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CrtDate { get; set; }

        public DateTime? TaskDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public Guid CreatorId { get; set; }

        public User CreatorUser { get; set; }

        public List<User> PerformingUsers { get; set; }

    }
}
