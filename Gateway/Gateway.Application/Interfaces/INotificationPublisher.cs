using Gateway.Domain.Entities;

namespace Gateway.Application.Interfaces;

public interface INotificationPublisher
{
    Task PublishAsync(Notification notification, CancellationToken cancellationToken = default);
}