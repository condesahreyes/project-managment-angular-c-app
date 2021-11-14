using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Configuration
{
    [ExcludeFromCodeCoverage]
    class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasKey(a => a.Id);
        }
    }
}
