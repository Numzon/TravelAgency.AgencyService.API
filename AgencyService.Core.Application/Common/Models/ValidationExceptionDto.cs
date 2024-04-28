namespace AgencyService.Core.Application.Common.Models;
public class ValidationExceptionDto : ExceptionDto
{
    public required IDictionary<string, string[]> Errors { get; set; }
}
