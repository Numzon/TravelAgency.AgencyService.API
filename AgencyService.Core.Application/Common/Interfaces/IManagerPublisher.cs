namespace AgencyService.Core.Application.Common.Interfaces;
public interface IManagerPublisher
{
    Task PublishManagerCreated(string email, string group, CancellationToken cancellationToken);
}
