using Gateway.Domain.Entities;

namespace Gateway.Application.Interfaces;

public interface INotificationService
{
    Task SendNotificationAsync(Notification notification, CancellationToken cancellationToken = default);

    Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}