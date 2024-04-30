namespace AgencyService.Adapter.RabbitMQ.Models;
public class TravelAgencyPublishedDto
{
    public required string UserId { get; set; }
    public required string AgencyName { get; set; }
}
