using AgencyService.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyService.Adapter.SQLServer.Persistance.Configurations;
public sealed class BankAccountDataConfiguration : IEntityTypeConfiguration<BankAccountData>
{
    public void Configure(EntityTypeBuilder<BankAccountData> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.AccountNumber)
            .IsRequired();

        builder.Property(x => x.Swift)
            .IsRequired();

        builder.Property(x => x.Ban)
            .IsRequired();
    }
}
