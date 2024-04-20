using AgencyService.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyService.Adapter.SQLServer.Persistance.Configurations;
public sealed class TravelAgencyAccountConfiguration : IEntityTypeConfiguration<TravelAgencyAccount>
{
    public void Configure(EntityTypeBuilder<TravelAgencyAccount> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(x => x.UserId)
            .IsRequired();
    }
}
