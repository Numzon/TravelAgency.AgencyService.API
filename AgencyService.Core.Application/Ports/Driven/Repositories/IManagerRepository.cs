using AgencyService.Core.Application.Common.Models;
using AgencyService.Core.Domain.Entities;

namespace AgencyService.Core.Application.Ports.Driven.Repositories;
public interface IManagerRepository
{
    Task<Manager> CreateAsync(CreateManagerDto managerDto, CancellationToken cancellationToken);
}
