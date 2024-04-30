using AgencyService.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyService.Adapter.SQLServer.Persistance.Configurations;
public sealed class TravelAgencyConfiguration : IEntityTypeConfiguration<TravelAgencyAccount>
{
    public void Configure(EntityTypeBuilder<TravelAgencyAccount> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.OwnsOne(x => x.CompanyData);

        builder.HasOne(x => x.BankAccountData)
            .WithOne(x => x.TravelAgency)
            .HasForeignKey<TravelAgencyAccount>(x => x.BankAccountDataId);
    }
}
