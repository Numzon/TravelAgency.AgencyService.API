using TravelAgency.SharedLibrary.Models;

namespace AgencyService.Adapter.RabbitMQ.Models;
public class ManagerCreatedPublishedDto : BasePublishedDto
{
    public required string Email { get; set; }
    public required string Group { get; set; }
}
