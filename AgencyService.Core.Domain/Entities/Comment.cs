namespace AgencyService.Core.Domain.Entities;
public sealed class Comment : BaseAuditableEntity
{
    public required string Text { get; set; }

    public int? TravelAgencyId { get; set; }
    public TravelAgencyAccount? TravelAgency { get; set; }

    public int? ManagerId { get; set; }
    public Manager? Manager { get; set; }

    public int? ParentCommentId { get; set; }
    public Comment? ParentComment { get; set; }

    public IReadOnlyCollection<Comment> Subcomments { get; set; } = new List<Comment>();
}
