using FluentValidation;

namespace AgencyService.Core.Application.Ports.Driving.TravelAgencies.Commands;
public sealed class CreateTravelAgencyCommandValidator : AbstractValidator<CreateTravelAgencyCommand>
{
    public CreateTravelAgencyCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}
