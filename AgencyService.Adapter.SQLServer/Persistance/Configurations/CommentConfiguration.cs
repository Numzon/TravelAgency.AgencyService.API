using AgencyService.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyService.Adapter.SQLServer.Persistance.Configurations;
public sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text)
            .IsRequired();

        builder.HasOne(x => x.TravelAgency)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.TravelAgencyId);

        builder.HasOne(x => x.Manager)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.ManagerId);

        builder.HasOne(x => x.ParentComment)
            .WithMany(x => x.Subcomments)
            .HasForeignKey(x => x.ParentCommentId);
    }
}
