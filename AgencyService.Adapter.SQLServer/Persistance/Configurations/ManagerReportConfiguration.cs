using AgencyService.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyService.Adapter.SQLServer.Persistance.Configurations;
public sealed class ManagerReportConfiguration : IEntityTypeConfiguration<ManagerReport>
{
    public void Configure(EntityTypeBuilder<ManagerReport> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Subject)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired();

        builder.HasOne(x => x.Manager)
            .WithMany(x => x.Reports)
            .HasForeignKey(x => x.ManagerId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(x => x.TravelAgency)
            .WithMany(x => x.Reports)
            .HasForeignKey(x => x.TravelAgencyId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
