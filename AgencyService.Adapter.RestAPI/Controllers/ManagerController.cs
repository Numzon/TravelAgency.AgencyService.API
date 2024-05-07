using AgencyService.Adapter.API.Models;
using AgencyService.Core.Application.Ports.Driving.Managers.Commands.CreateManager;
using AgencyService.Core.Application.Ports.Driving.Managers.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyService.Adapter.API.Controllers;
[ApiController]
[Route("api/managers")]
public sealed class ManagerController : ControllerBase
{
    private readonly ISender _sender;

    public ManagerController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IResult> CreateAsync([FromBody]CreateManagerCommand command, CancellationToken cancellationToken = default)
    {
        var agency = await _sender.Send(command, cancellationToken);

        return Results.CreatedAtRoute(nameof(GetAsync), new { id = agency.Id }, agency.Adapt<SimpleManagerDto>());
    }

    [HttpGet("{id}", Name = nameof(GetAsync))]
    public async Task<IResult> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var manager = await _sender.Send(new GetManagerQuery(id), cancellationToken);
        
        var dto = manager.Adapt<ManagerDto>();

        return Results.Ok(dto);
    }
}
