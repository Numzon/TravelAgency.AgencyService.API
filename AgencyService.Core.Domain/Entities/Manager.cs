namespace AgencyService.Core.Domain.Entities;
public sealed class Manager : BaseAuditableEntity
{
    public string? UserId { get; set; }
    public required string Email { get; set; }
    public required PersonalData PersonalData { get; set; }

    public int TravelAgencyId { get; set; }
    public required TravelAgencyAccount TravelAgency { get; set; }

    public IReadOnlyCollection<Comment> Comments { get; set; } = new List<Comment>();
    public IReadOnlyCollection<ManagerReport> Reports { get; set; } = new List<ManagerReport>();
}
