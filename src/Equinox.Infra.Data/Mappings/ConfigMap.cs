using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Equinox.Domain.Models;

namespace Equinox.Infra.Data.Mappings
{
    public class ConfigMap : IEntityTypeConfiguration<Config>
    {
        public void Configure(EntityTypeBuilder<Config> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.SystemEnable)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(c => c.Currency)
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(10)
                .IsRequired();
        }
    }
}