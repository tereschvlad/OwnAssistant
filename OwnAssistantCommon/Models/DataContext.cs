using Microsoft.EntityFrameworkCore;


namespace OwnAssistantCommon.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option) 
        {

        }

        public DbSet<UserDbModel> Users { get; set; }

        public DbSet<RoleDbModel> Roles { get; set; }

        public DbSet<CustomerTaskMainInfoDbModel> MainInfoTasks { get; set; }

        public DbSet<CustomerTaskDateInfoDbModel> DateInfoTasks { get; set; }

        public DbSet<CustomerTaskCheckpointInfoDbModel> CheckpointInfoTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Roles
            modelBuilder.Entity<RoleDbModel>().ToTable("Roles");

            //Users
            modelBuilder.Entity<UserDbModel>().ToTable("Users");
            modelBuilder.Entity<UserDbModel>().HasOne<RoleDbModel>(x => x.Role).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<UserDbModel>().HasMany<CustomerTaskMainInfoDbModel>(x => x.CreatedTasks).WithOne(x => x.CreatorUser).HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserDbModel>().HasMany<CustomerTaskMainInfoDbModel>(x => x.PerformingTasks).WithOne(x => x.PerformingUser).HasForeignKey(x => x.PerformerId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserDbModel>().Property(x => x.CrtDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            //Main info about tasks
            modelBuilder.Entity<CustomerTaskMainInfoDbModel>().ToTable("MainInfoTasks");
            modelBuilder.Entity<CustomerTaskMainInfoDbModel>().HasOne<UserDbModel>(x => x.CreatorUser).WithMany(x => x.CreatedTasks).HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CustomerTaskMainInfoDbModel>().HasOne<UserDbModel>(x => x.PerformingUser).WithMany(x => x.PerformingTasks).HasForeignKey(x => x.PerformerId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CustomerTaskMainInfoDbModel>().HasMany<CustomerTaskDateInfoDbModel>(x => x.CustomerTaskDateInfos).WithOne(x => x.CustomerTaskMainInfo).HasForeignKey(x => x.CustomerTaskMainId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomerTaskMainInfoDbModel>().HasMany<CustomerTaskCheckpointInfoDbModel>(x => x.CustomerTaskCheckpointInfos).WithOne(x => x.CustomerTaskMainInfo).HasForeignKey(x => x.CustomerTaskMainId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomerTaskMainInfoDbModel>().Property(x => x.CrtDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            //Date info about tasks
            modelBuilder.Entity<CustomerTaskDateInfoDbModel>().ToTable("DateInfoTasks");

            //Checkpoint info about tasks
            modelBuilder.Entity<CustomerTaskCheckpointInfoDbModel>().ToTable("CheckpointInfoTasks");
            modelBuilder.Entity<CustomerTaskCheckpointInfoDbModel>().Property(x => x.Lat).HasColumnType("numeric(26, 20)");
            modelBuilder.Entity<CustomerTaskCheckpointInfoDbModel>().Property(x => x.Long).HasColumnType("numeric(26, 20)");
        }
    }

    /// <summary>
    /// Db model for project user
    /// </summary>
    public class UserDbModel
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime CrtDate { get; set; }

        public Guid? RoleId { get; set; }

        public RoleDbModel Role { get; set; }

        public long TelegramId { get; set; }

        public List<CustomerTaskMainInfoDbModel> CreatedTasks { get; set; }

        public List<CustomerTaskMainInfoDbModel> PerformingTasks { get; set; }
    }

    /// <summary>
    /// Db model for role by user
    /// </summary>
    public class RoleDbModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    /// <summary>
    /// Db model for tasks (main info)
    /// </summary>
    public class CustomerTaskMainInfoDbModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CrtDate { get; set; }

        public Guid CreatorId { get; set; }

        public UserDbModel CreatorUser { get; set; }

        public Guid PerformerId { get; set; }

        public UserDbModel PerformingUser { get; set; }

        public List<CustomerTaskDateInfoDbModel> CustomerTaskDateInfos { get; set; }

        public List<CustomerTaskCheckpointInfoDbModel> CustomerTaskCheckpointInfos { get; set; }
    }

    /// <summary>
    /// Db model for tasks (date info)
    /// </summary>
    public class CustomerTaskDateInfoDbModel
    {
        public Guid Id { get; set; }

        public Guid CustomerTaskMainId { get; set; }

        public CustomerTaskMainInfoDbModel CustomerTaskMainInfo { get; set; }

        public DateTime TaskDate { get; set; }
    }

    /// <summary>
    /// Db model for tasks (checkpoint info)
    /// </summary>
    public class CustomerTaskCheckpointInfoDbModel
    {
        public Guid Id { get; set; }

        public Guid CustomerTaskMainId { get; set; }

        public CustomerTaskMainInfoDbModel CustomerTaskMainInfo { get; set; }

        public decimal Lat { get; set; }

        public decimal Long { get; set; }
    }
}
