using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taskill.Domain;

namespace Taskill.Database;

public class SectionConfig : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> section)
    {
        section.ToTable("sections");

        section.HasKey(s => s.Id);
        section.Property(s => s.Id).ValueGeneratedOnAdd();

        section.HasMany(s => s.Tasks)
            .WithOne()
            .HasForeignKey(t => t.SectionId);
    }
}
