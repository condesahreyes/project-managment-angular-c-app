using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace DataAccess.Configuration
{
    class BugConfiguration : IEntityTypeConfiguration<Bug>
    {
        public void Configure(EntityTypeBuilder<Bug> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(b => b.State);
            builder.HasOne(b => b.Project).WithMany(p => p.Bugs).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
