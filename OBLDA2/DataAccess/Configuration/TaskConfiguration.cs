using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace DataAccess.Configuration
{
    class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(a => a.Id);
            builder.HasOne(b => b.Project).WithMany(p => p.Tasks).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
