using Core.Domain.Abstractions;
using Core.Domain.Enums;
using Core.Domain.Events;
using Gateway.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Gateway.Infrastructure.Consumers;

public class NotificationSentConsumer(
    IRepository<Notification, Guid> repository,
    IUnitOfWork unitOfWork,
    ILogger<NotificationSentConsumer> logger) 
    : IConsumer<NotificationSentEvent>
{
    public async Task Consume(ConsumeContext<NotificationSentEvent> context)
    {
        var message = context.Message;
        
        logger.LogInformation("Gateway received confirmation for Notification {Id}", message.NotificationId);

        var notification = await repository.GetByIdAsync(message.NotificationId, context.CancellationToken);

        if (notification is null)
        {
            logger.LogWarning("Notification {Id} not found in DB", message.NotificationId);
            return;
        }

        notification.Status = NotificationStatus.Sent;

        repository.Update(notification);
        await unitOfWork.SaveChangesAsync(context.CancellationToken);
        
        logger.LogInformation("Notification {Id} status updated to SENT", message.NotificationId);
    }
}