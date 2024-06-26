﻿using AgencyService.Adapter.SQLServer.Interfaces;
using AgencyService.Core.Application.Ports.Driven;
using AgencyService.Core.Domain.Entities;
using AgencyService.Core.Domain.Enums;
using AgencyService.Core.Domain.ValueObjects;

namespace AgencyService.Adapter.SQLServer.Repositories;
public class TravelAgencyRepository : ITravelAgencyRepository
{
    private readonly IAgencyServiceDbContext _context;

    public TravelAgencyRepository(IAgencyServiceDbContext context)
    {
        _context = context;
    }

    public async Task<TravelAgencyAccount> CreateAsync(string userId, string agencyName, CancellationToken cancellationToken)
    {
        var agency = new TravelAgencyAccount
        {
            UserId = userId,
            CompanyData = new CompanyData(agencyName),
            Status = TravelAgencyStatus.Unverified
        };

        await _context.TravelAgencyAccount.AddAsync(agency, cancellationToken);
        await _context.SaveChangesAsync();

        return agency;
    }

}
