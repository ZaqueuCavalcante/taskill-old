using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taskill.Domain;
using Taskill.Extensions;
using Taskill.Settings;

namespace Taskill.Database;

public class TaskillDbContext : IdentityDbContext<Taskiller, IdentityRole<uint>, uint>
{
    public DbSet<Domain.Task> Tasks { get; set; }
    public DbSet<Subtask> Subtasks { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Label> Labels { get; set; }
    public DbSet<Reminder> Reminders { get; set; }

    public DbSet<Taskiller> Taskillers { get; set; }

    public TaskillDbContext(DbContextOptions<TaskillDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (Env.IsDevelopment())
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("taskill");

        builder.Entity<Taskiller>().ToTable("taskillers");
        builder.Entity<IdentityUserClaim<uint>>().ToTable("claims");

        builder.ChangeIdentityTablesToSnakeCase();

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
