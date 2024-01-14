using Microsoft.EntityFrameworkCore;


namespace OwnAssistantCommon.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option) 
        {

        }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<RoleModel> Roles { get; set; }

        public DbSet<CustomerTaskModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Table Roles
            modelBuilder.Entity<RoleModel>().ToTable("Roles");

            //Table Users
            modelBuilder.Entity<UserModel>().ToTable("Users");
            modelBuilder.Entity<UserModel>().HasOne<RoleModel>(x => x.Role).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<UserModel>().HasMany<CustomerTaskModel>(x => x.CreatedTasks).WithOne(x => x.CreatorUser).HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserModel>().HasMany<CustomerTaskModel>(x => x.PerformingTasks).WithMany(x => x.PerformingUsers);
            modelBuilder.Entity<UserModel>().Property(x => x.CrtDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            //Table Tasks
            modelBuilder.Entity<CustomerTaskModel>().ToTable("Tasks");
            modelBuilder.Entity<CustomerTaskModel>().HasOne<UserModel>(x => x.CreatorUser).WithMany(x => x.CreatedTasks).HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CustomerTaskModel>().HasMany<UserModel>(x => x.PerformingUsers).WithMany(x => x.PerformingTasks);
            modelBuilder.Entity<CustomerTaskModel>().Property(x => x.CrtDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }

    /// <summary>
    /// Db model for project user
    /// </summary>
    public class UserModel
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime CrtDate { get; set; }

        public Guid? RoleId { get; set; }

        public RoleModel Role { get; set; }

        public List<CustomerTaskModel> CreatedTasks { get; set; }

        public List<CustomerTaskModel> PerformingTasks { get; set; }
    }

    /// <summary>
    /// Db model for role by user
    /// </summary>
    public class RoleModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    /// <summary>
    /// Db model for tasks
    /// </summary>
    public class CustomerTaskModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CrtDate { get; set; }

        public DateTime? TaskDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public Guid CreatorId { get; set; }

        public UserModel CreatorUser { get; set; }

        public List<UserModel> PerformingUsers { get; set; }

    }
}
