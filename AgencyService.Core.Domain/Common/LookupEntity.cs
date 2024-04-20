namespace AgencyService.Core.Domain.Common;
public abstract class LookupEntity : BaseAuditableEntity
{
    public required string Name { get; set; }
}
