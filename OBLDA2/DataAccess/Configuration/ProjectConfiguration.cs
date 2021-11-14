using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Configuration
{
    [ExcludeFromCodeCoverage]
    class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasMany(p => p.Bugs).WithOne(b => b.Project).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(t => t.Tasks).WithOne(b => b.Project).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Users)
            .WithMany(c => c.Projects).UsingEntity(j => j.ToTable("UsersProject"));
        }
    }
}
