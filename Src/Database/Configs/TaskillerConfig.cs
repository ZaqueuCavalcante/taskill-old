using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskill.Domain;

namespace Taskill.Database;

public class TaskillerConfig : IEntityTypeConfiguration<Taskiller>
{
    public void Configure(EntityTypeBuilder<Taskiller> taskiller)
    {
        taskiller.HasMany(t => t.Projects)
            .WithOne()
            .HasForeignKey(p => p.UserId);

        taskiller.HasMany(t => t.Tasks)
            .WithOne()
            .HasForeignKey(t => t.UserId);

        taskiller.HasMany(t => t.Labels)
            .WithOne()
            .HasForeignKey(l => l.UserId);
    }
}
