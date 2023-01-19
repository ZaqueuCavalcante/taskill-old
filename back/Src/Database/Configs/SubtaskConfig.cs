using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskill.Domain;

namespace Taskill.Database;

public class SubtaskConfig : IEntityTypeConfiguration<Subtask>
{
    public void Configure(EntityTypeBuilder<Subtask> subtask)
    {
        subtask.ToTable("subtasks");

        subtask.HasKey(s => s.Id);
        subtask.Property(s => s.Id).ValueGeneratedOnAdd();

        subtask.Property(s => s.TaskId).IsRequired();

        subtask.Property(s => s.Title).IsRequired();

        subtask.HasOne<Domain.Task>()
            .WithMany(t => t.Subtasks)
            .HasForeignKey(s => s.TaskId);
    }
}
