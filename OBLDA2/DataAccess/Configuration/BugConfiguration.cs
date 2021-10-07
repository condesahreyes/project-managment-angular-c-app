using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Configuration
{
    class BugConfiguration : IEntityTypeConfiguration<Bug>
    {
        public void Configure(EntityTypeBuilder<Bug> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedNever();
            builder.HasOne(b => b.State);
            builder.HasOne(b => b.Project).WithMany(p => p.Bugs).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
