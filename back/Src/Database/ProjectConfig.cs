using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taskill.Domain;

namespace Taskill.Database;

public class ProjectConfig : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> project)
    {
        project.ToTable("projects");

        project.HasKey(p => p.Id);
        project.Property(p => p.Id).ValueGeneratedOnAdd();

        project.Property(p => p.UserId).IsRequired();

        project.Property(p => p.Layout)
            .HasConversion(new EnumToStringConverter<Layout>());

        project.HasMany(p => p.Sections)
            .WithOne()
            .HasForeignKey(s => s.ProjectId)
            .IsRequired();

        project.HasMany(p => p.Tasks)
            .WithOne()
            .HasForeignKey(t => t.ProjectId)
            .IsRequired();
    }
}
