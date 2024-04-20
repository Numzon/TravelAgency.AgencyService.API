using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AgencyService.Adapter.SQLServer.Persistance;
public sealed class AgencyServiceDbContextInitialiser
{
    private readonly AgencyServiceDbContext _agencyServiceDbContext;

    public AgencyServiceDbContextInitialiser(AgencyServiceDbContext agencyServiceDbContext)
    {
        _agencyServiceDbContext = agencyServiceDbContext;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_agencyServiceDbContext.Database.IsSqlServer())
            {
                await _agencyServiceDbContext.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            Log.Error("An error occurred while initialising the database.", ex);
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        try
        {
            await SeedAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            throw;
        }
    }

    public Task SeedAsync()
    {
        return Task.CompletedTask;
    }
}
