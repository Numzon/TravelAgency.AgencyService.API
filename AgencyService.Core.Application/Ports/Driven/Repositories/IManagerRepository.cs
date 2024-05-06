using AgencyService.Core.Application.Ports.Driving.Managers.Commands.CreateManager;
using AgencyService.Core.Domain.Entities;

namespace AgencyService.Core.Application.Ports.Driven.Repositories;
public interface IManagerRepository
{
    Task<Manager> CreateAsync(CreateManagerCommand request, CancellationToken cancellationToken);
    Task UpdateUserIdAsync(int managerId, string userId, CancellationToken cancellationToken); 
    Task<Manager?> GetManagerAsync(int managerId, CancellationToken cancellationToken);
}
