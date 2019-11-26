using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usavc.Services.Appointment.API.Model;

namespace Usavc.Services.Appointment.API.Infrastructure.EntityConfigurations
{
    class CatalogBrandEntityTypeConfiguration
        : IEntityTypeConfiguration<ReasonCode>
    {
        public void Configure(EntityTypeBuilder<ReasonCode> builder)
        {
            builder.ToTable("ReasonCode");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
               .ForSqlServerUseSequenceHiLo("reason_code_hilo")
               .IsRequired();

            builder.Property(cb => cb.Code)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
