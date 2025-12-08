using Core.Domain.Abstractions;
using Gateway.Application.Interfaces;
using Gateway.Domain.Entities;

namespace Gateway.Application.Services;

public class NotificationService(
    IRepository<Notification, Guid> repository,
    IUnitOfWork unitOfWork,
    INotificationPublisher publisher)
    : INotificationService
{
    public async Task SendNotificationAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        await repository.AddAsync(notification, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await publisher.PublishAsync(notification, cancellationToken);
    }

    public async Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await repository.GetByIdAsync(id, cancellationToken);
    }
}