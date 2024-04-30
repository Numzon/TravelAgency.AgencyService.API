namespace AgencyService.Core.Domain.Entities;
public class TravelAgencyAccount : BaseAuditableEntity
{
    public required string UserId { get; set; }
    public required CompanyData CompanyData { get; set; }
    public required TravelAgencyStatus Status { get; set; }

    public int? BankAccountDataId { get; set; }
    public BankAccountData? BankAccountData { get; set; }

    public IReadOnlyCollection<Manager> Managers { get; set; } = new List<Manager>();
    public IReadOnlyCollection<Comment> Comments { get; set; } = new List<Comment>();
    public IReadOnlyCollection<ManagerReport> Reports { get; set; } = new List<ManagerReport>();

}
