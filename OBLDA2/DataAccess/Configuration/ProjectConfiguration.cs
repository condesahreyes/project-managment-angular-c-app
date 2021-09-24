using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace DataAccess.Configuration
{
    class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasMany(p => p.Bugs).WithOne(b => b.Project).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Users)
            .WithMany(c => c.Projects).UsingEntity(j => j.ToTable("UsersProject"));
        }
    }
}
