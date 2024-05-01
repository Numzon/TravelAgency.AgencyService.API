using AgencyService.Adapter.SQLServer.Interfaces;
using AgencyService.Core.Application.Common.Models;
using AgencyService.Core.Application.Ports.Driven.Repositories;
using AgencyService.Core.Domain.Entities;
using Mapster;

namespace AgencyService.Adapter.SQLServer.Repositories;
public sealed class ManagerRepository : IManagerRepository
{
    private readonly IAgencyServiceDbContext _context;

    public ManagerRepository(IAgencyServiceDbContext context)
    {
        _context = context;
    }

    public async Task<Manager> CreateAsync(CreateManagerDto managerDto, CancellationToken cancellationToken)
    {
        var manager = managerDto.Adapt<Manager>();

        await _context.Manager.AddAsync(manager);
        await _context.SaveChangesAsync();

        return manager;
    }
}
