using TaskManagementAPI.Models;
namespace TeamTaskManagementAPI.Data;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamUser> TeamUsers { get; set; }
    public DbSet<TaskManagementAPI.Models.Task> Tasks { get; set; }
    public DbSet<LogEntry> Logs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure TeamUser (composite key)
        modelBuilder.Entity<TeamUser>()
            .HasKey(tu => new { tu.UserId, tu.TeamId });

        modelBuilder.Entity<TeamUser>()
            .HasOne(tu => tu.User)
            .WithMany(u => u.TeamUsers)
            .HasForeignKey(tu => tu.UserId);

        modelBuilder.Entity<TeamUser>()
            .HasOne(tu => tu.Team)
            .WithMany(t => t.TeamUsers)
            .HasForeignKey(tu => tu.TeamId);

        // Configure Task relationships
        modelBuilder.Entity<TaskManagementAPI.Models.Task>()
            .HasOne(t => t.AssignedToUser)
            .WithMany(u => u.AssignedTasks)
            .HasForeignKey(t => t.AssignedToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskManagementAPI.Models.Task>()
            .HasOne(t => t.CreatedByUser)
            .WithMany(u => u.CreatedTasks)
            .HasForeignKey(t => t.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskManagementAPI.Models.Task>()
            .HasOne(t => t.Team)
            .WithMany(team => team.Tasks)
            .HasForeignKey(t => t.TeamId);

        // Enum conversion (optional): Store TaskStatus as string
        modelBuilder.Entity<TaskManagementAPI.Models.Task>()
            .Property(t => t.Status)
            .HasConversion<string>();
    }
}

