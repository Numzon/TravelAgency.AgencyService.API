﻿using TravelAgency.SharedLibrary.Models;

namespace AgencyService.Adapter.RabbitMQ.Models;
public class ManagerPublishedDto : BasePublishedDto
{
    public required string UserId { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Group { get; set; }
    public required int TravelAgencyId { get; set; }
}