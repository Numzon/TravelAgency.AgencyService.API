namespace AgencyService.Core.Domain.Events;
public sealed class ManagerCreatedEvent : BaseEvent
{
    public required int ManagerId { get; set; }
    public required string Email { get; set; }
    public required string Group { get; set; }
}
