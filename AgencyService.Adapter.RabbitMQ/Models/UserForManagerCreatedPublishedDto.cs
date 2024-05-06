using TravelAgency.SharedLibrary.Models;

namespace AgencyService.Adapter.RabbitMQ.Models;
public class UserForManagerCreatedPublishedDto : BasePublishedDto
{
    public required int ManagerId { get; set; }
    public required string UserId { get; set; }
}