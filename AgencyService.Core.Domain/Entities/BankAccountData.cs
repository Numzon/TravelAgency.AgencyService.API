namespace AgencyService.Core.Domain.Entities;
public sealed class BankAccountData : BaseAuditableEntity
{
    public required string AccountNumber { get; set; }
    public required string Ban { get; set; }
    public required string Swift { get; set; }

    public required TravelAgencyAccount TravelAgency { get; set; }
}
