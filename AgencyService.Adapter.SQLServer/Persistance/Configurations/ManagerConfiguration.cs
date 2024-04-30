using AgencyService.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyService.Adapter.SQLServer.Persistance.Configurations;
public sealed class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.OwnsOne(x => x.PersonalData);

        builder.HasOne(x => x.TravelAgency)
            .WithMany(x => x.Managers)
            .HasForeignKey(x => x.TravelAgencyId)
            .IsRequired();
    }
}
