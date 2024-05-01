using TravelAgency.SharedLibrary.Models;

namespace AgencyService.Adapter.RabbitMQ.Models;
public class TravelAgencyPublishedDto : BasePublishedDto
{
    public required string UserId { get; set; }
    public required string AgencyName { get; set; }
}
