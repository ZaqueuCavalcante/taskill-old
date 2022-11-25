using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskill.Domain;

namespace Taskill.Database;

public class LabelConfig : IEntityTypeConfiguration<Label>
{
    public void Configure(EntityTypeBuilder<Label> label)
    {
        label.ToTable("labels");

        label.HasKey(l => l.Id);

        label.Property(l => l.UserId).IsRequired();
    }
}
