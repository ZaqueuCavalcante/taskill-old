using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskill.Domain;

namespace Taskill.Database;

public class SubtaskConfig : IEntityTypeConfiguration<Subtask>
{
    public void Configure(EntityTypeBuilder<Subtask> subtask)
    {
        subtask.ToTable("tasks");

        subtask.HasOne<Domain.Task>()
            .WithMany()
            .HasForeignKey(s => s.ParentTaskId);
    }
}
