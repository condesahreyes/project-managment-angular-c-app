using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace DataAccess.Configuration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasMany(p => p.Projects)
                .WithMany(u => u.Users).UsingEntity(j => j.ToTable("UsersProject"));
        }
    }
}
