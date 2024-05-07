namespace AgencyService.Core.Application.Common.Interfaces;
public interface IManagerPublisher
{
    Task PublishManagerCreated(int managerId, string email, string group, CancellationToken cancellationToken);
}
