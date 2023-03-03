using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taskill.Domain;
using Action = Taskill.Domain.Action;
using Task = Taskill.Domain.Task;

namespace Taskill.Database;

public class ActionConfig : IEntityTypeConfiguration<Action>
{
    public void Configure(EntityTypeBuilder<Action> action)
    {
        action.ToTable("actions");

        action.HasKey(a => a.Id);
        action.Property(a => a.Id).ValueGeneratedOnAdd();

        action.Property(a => a.Type)
            .HasConversion(new EnumToStringConverter<ActionType>());

        action.HasOne<Taskiller>()
            .WithMany()
            .HasForeignKey(a => a.UserId);

        action.HasOne<Project>()
            .WithMany()
            .HasForeignKey(a => a.ProjectId);

        action.HasOne<Label>()
            .WithMany()
            .HasForeignKey(a => a.LabelId);
    }
}
