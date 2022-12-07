using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Taskill.Domain;
using Taskill.Extensions;
using static Taskill.Configs.AuthorizationConfigs;

namespace Taskill.Database;

public class TaskillDbContext : IdentityDbContext<Taskiller, IdentityRole<uint>, uint>
{
    public DbSet<Domain.Task> Tasks { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Label> Labels { get; set; }

    public TaskillDbContext(DbContextOptions<TaskillDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("taskill");

        builder.ChangeIdentityTablesToSnakeCase();

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        builder.Entity<IdentityRole<uint>>().HasData(
            new IdentityRole<uint>()
            {
                Id = 1,
                Name = TaskillerRole,
                NormalizedName = TaskillerRole.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            });
    }
}
