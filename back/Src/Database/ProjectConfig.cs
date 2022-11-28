using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        project.HasMany(p => p.Tasks)
            .WithOne()
            .HasForeignKey(t => t.ProjectId)
            .IsRequired();
    }
}
