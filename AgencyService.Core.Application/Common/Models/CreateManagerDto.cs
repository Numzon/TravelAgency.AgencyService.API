namespace AgencyService.Core.Application.Common.Models;
public class CreateManagerDto
{
    public required string UserId { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Group { get; set; }
    public required int TravelAgencyId { get; set; }
}
