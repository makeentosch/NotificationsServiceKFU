using Core.Domain.Abstractions;
using Core.Domain.Enums;
using Core.Domain.Events;
using Gateway.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Gateway.Infrastructure.Consumers;

public class NotificationFaultConsumer(
    IRepository<Notification, Guid> repository,
    IUnitOfWork unitOfWork,
    ILogger<NotificationFaultConsumer> logger)
    : IConsumer<Fault<NotificationCreatedEvent>>
{
    public async Task Consume(ConsumeContext<Fault<NotificationCreatedEvent>> context)
    {
        var originalMessage = context.Message.Message;
        var exceptions = context.Message.Exceptions;

        var errorMessage = exceptions.FirstOrDefault()?.Message ?? "Unknown error";

        logger.LogError("Gateway received fault for notification {id} with {reason}",
            originalMessage.Id, errorMessage);

        var notification = await repository.GetByIdAsync(originalMessage.Id, context.CancellationToken);

        if (notification is null)
            return;

        if (notification.Status != NotificationStatus.Failed)
        {
            notification.Status = NotificationStatus.Failed;

            repository.Update(notification);
            await unitOfWork.SaveChangesAsync(context.CancellationToken);

            logger.LogInformation("Notification {id} status updated to FAILED", originalMessage.Id);
        }
    }
}