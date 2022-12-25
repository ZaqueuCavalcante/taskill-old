using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskill.Domain;
using Task = Taskill.Domain.Task;

namespace Taskill.Database;

public class ReminderConfig : IEntityTypeConfiguration<Reminder>
{
    public void Configure(EntityTypeBuilder<Reminder> reminder)
    {
        reminder.ToTable("reminders");

        reminder.HasKey(r => r.Id);
        reminder.Property(r => r.Id).ValueGeneratedOnAdd();

        reminder.HasOne<Task>()
            .WithMany()
            .HasForeignKey(r => r.TaskId);
    }
}
