namespace AgencyService.Core.Domain.Entities;
public sealed class ManagerReport : BaseAuditableEntity
{
    public required string Subject { get; set; }
    public required string Description { get; set; }

    public int ManagerId { get; set; }
    public required Manager Manager { get; set; }

    public int TravelAgencyId { get; set; }
    public required TravelAgencyAccount TravelAgency { get; set; }
}
