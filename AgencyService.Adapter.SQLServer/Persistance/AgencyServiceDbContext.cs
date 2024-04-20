using AgencyService.Adapter.SQLServer.Extensions;
using AgencyService.Adapter.SQLServer.Interfaces;
using AgencyService.Adapter.SQLServer.Persistance.Interceptors;
using AgencyService.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AgencyService.Adapter.SQLServer.Persistance;
public sealed class AgencyServiceDbContext : DbContext, IAgencyServiceDbContext
{
    private readonly IMediator _mediator;
    private readonly BaseAuditableEntitySaveChangesInterceptor _baseAuditableEntitySaveChangesInterceptor;

    public DbSet<TravelAgencyAccount> TravelAgencyAccount { get; set; }

    public AgencyServiceDbContext(DbContextOptions<AgencyServiceDbContext> options,
       IMediator mediator,
       BaseAuditableEntitySaveChangesInterceptor baseAuditableEntitySaveChangesInterceptor) : base(options)
    {
        _baseAuditableEntitySaveChangesInterceptor = baseAuditableEntitySaveChangesInterceptor;
        _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_baseAuditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
