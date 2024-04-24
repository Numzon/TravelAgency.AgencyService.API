using AgencyService.Adapter.API.Models;
using AgencyService.Core.Application.Ports.Driving.TravelAgencies.Commands;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyService.Adapter.API.Controllers;

[ApiController]
[Route("api/travel-agencies")]
public sealed class TravelAgencyController : ControllerBase
{
    private readonly ISender _sender;

    public TravelAgencyController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IResult> CreateAsync(CreateTravelAgencyCommand command, CancellationToken cancellationToken = default)
    {
        var agency = await _sender.Send(command, cancellationToken);

        return Results.CreatedAtRoute(nameof(GetAsync), new { id = agency.Id }, agency.Adapt<TravelAgencyDto>());
    }

    [HttpGet("{id}", Name = nameof(GetAsync))]
    public async Task<IResult> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        //var agency = await _sender.Send(command, cancellationToken);

        return Results.Ok();
    }
}
