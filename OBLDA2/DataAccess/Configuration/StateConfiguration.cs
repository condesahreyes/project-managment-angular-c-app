using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Configuration
{
    [ExcludeFromCodeCoverage]
    class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(a => a.Name);
        }
    }
}
