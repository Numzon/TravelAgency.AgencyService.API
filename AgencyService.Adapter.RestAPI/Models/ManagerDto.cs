namespace AgencyService.Adapter.API.Models;
public class ManagerDto
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Group { get; set; }
    public required int TravelAgencyId { get; set; }
}
