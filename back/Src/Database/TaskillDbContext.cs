using Microsoft.EntityFrameworkCore;
using Taskill.Domain;

namespace Taskill.Database;

public class TaskillDbContext : DbContext
{
    public DbSet<Domain.Task> Tasks { get; set; }
    public DbSet<Project> Projects { get; set; }

    public TaskillDbContext(DbContextOptions<TaskillDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("taskill");

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
