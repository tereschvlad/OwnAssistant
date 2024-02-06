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

        public DbSet<CustomerTaskMainInfoModel> MainInfoTasks { get; set; }

        public DbSet<CustomerTaskDateInfoModel> DateInfoTasks { get; set; }

        public DbSet<CustomerTaskCheckpointInfo> CheckpointInfoTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Roles
            modelBuilder.Entity<RoleModel>().ToTable("Roles");

            //Users
            modelBuilder.Entity<UserModel>().ToTable("Users");
            modelBuilder.Entity<UserModel>().HasOne<RoleModel>(x => x.Role).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<UserModel>().HasMany<CustomerTaskMainInfoModel>(x => x.CreatedTasks).WithOne(x => x.CreatorUser).HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserModel>().HasMany<CustomerTaskMainInfoModel>(x => x.PerformingTasks).WithOne(x => x.PerformingUser).HasForeignKey(x => x.PerformerId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserModel>().Property(x => x.CrtDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            //Main info about tasks
            modelBuilder.Entity<CustomerTaskMainInfoModel>().ToTable("MainInfoTasks");
            modelBuilder.Entity<CustomerTaskMainInfoModel>().HasOne<UserModel>(x => x.CreatorUser).WithMany(x => x.CreatedTasks).HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CustomerTaskMainInfoModel>().HasOne<UserModel>(x => x.PerformingUser).WithMany(x => x.PerformingTasks).HasForeignKey(x => x.PerformerId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CustomerTaskMainInfoModel>().HasMany<CustomerTaskDateInfoModel>(x => x.CustomerTaskDateInfos).WithOne(x => x.CustomerTaskMainInfo).HasForeignKey(x => x.CustomerTaskMainId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomerTaskMainInfoModel>().HasMany<CustomerTaskCheckpointInfo>(x => x.CustomerTaskCheckpointInfos).WithOne(x => x.CustomerTaskMainInfo).HasForeignKey(x => x.CustomerTaskMainId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomerTaskMainInfoModel>().Property(x => x.CrtDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            //Date info about tasks
            modelBuilder.Entity<CustomerTaskDateInfoModel>().ToTable("DateInfoTasks");

            //Checkpoint info about tasks
            modelBuilder.Entity<CustomerTaskCheckpointInfo>().ToTable("CheckpointInfoTasks");
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

        public List<CustomerTaskMainInfoModel> CreatedTasks { get; set; }

        public List<CustomerTaskMainInfoModel> PerformingTasks { get; set; }
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
    /// Db model for tasks (main info)
    /// </summary>
    public class CustomerTaskMainInfoModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CrtDate { get; set; }

        public Guid CreatorId { get; set; }

        public UserModel CreatorUser { get; set; }

        public Guid PerformerId { get; set; }

        public UserModel PerformingUser { get; set; }

        public List<CustomerTaskDateInfoModel> CustomerTaskDateInfos { get; set; }

        public List<CustomerTaskCheckpointInfo> CustomerTaskCheckpointInfos { get; set; }
    }

    /// <summary>
    /// Db model for tasks (date info)
    /// </summary>
    public class CustomerTaskDateInfoModel
    {
        public Guid Id { get; set; }

        public Guid CustomerTaskMainId { get; set; }

        public CustomerTaskMainInfoModel CustomerTaskMainInfo { get; set; }

        public DateTime? TaskDate { get; set; }

        public DateTime NoteDate { get; set; }

        public string Note { get; set; }
    }

    /// <summary>
    /// Db model for tasks (checkpoint info)
    /// </summary>
    public class CustomerTaskCheckpointInfo
    {
        public Guid Id { get; set; }

        public Guid CustomerTaskMainId { get; set; }

        public CustomerTaskMainInfoModel CustomerTaskMainInfo { get; set; }

        public string Note { get; set; }

        public decimal Lat { get; set; }

        public decimal Long { get; set; }
    }
}
