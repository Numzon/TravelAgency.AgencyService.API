using AgencyService.Adapter.SQLServer.Interfaces;
using AgencyService.Core.Application.Common.Exceptions;
using AgencyService.Core.Application.Ports.Driven.Repositories;
using AgencyService.Core.Application.Ports.Driving.Managers.Commands.CreateManager;
using AgencyService.Core.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AgencyService.Adapter.SQLServer.Repositories;
public sealed class ManagerRepository : IManagerRepository
{
    private readonly IAgencyServiceDbContext _context;

    public ManagerRepository(IAgencyServiceDbContext context)
    {
        _context = context;
    }

    public async Task<Manager> CreateAsync(CreateManagerCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var manager = request.Adapt<Manager>();

        await _context.Manager.AddAsync(manager);
        await _context.SaveChangesAsync();

        return manager;
    }

    public async Task UpdateUserIdAsync(int managerId, string userId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var manager = await GetManagerAsync(managerId, cancellationToken);

        if (manager == null)
        {
            var message = $"Manager with given Id doesn't exist. Method Name: {nameof(UpdateUserIdAsync)}";

            Log.Error(message);
            throw new NotFoundException(message);
        }

        manager.UserId = userId;

        _context.Manager.Entry(manager);
        await _context.SaveChangesAsync();
    }
    public async Task<Manager?> GetManagerAsync(int managerId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await _context.Manager.FirstOrDefaultAsync(x => x.Id == managerId);
    }
}
