using Core.Domain.Events;
using Gateway.Application.Interfaces;
using Gateway.Domain.Entities;
using MassTransit;

namespace Gateway.Infrastructure.Messaging;

public class MassTransitPublisher(IPublishEndpoint publishEndpoint) : INotificationPublisher
{
    public async Task PublishAsync(Notification notification, CancellationToken cancellationToken = default)
    {
        var eventMessage = new NotificationCreatedEvent(
            notification.Id,
            notification.Type,
            notification.RecipientContact,
            notification.Subject,
            notification.Content
        );

        await publishEndpoint.Publish(eventMessage, cancellationToken);
    }
}