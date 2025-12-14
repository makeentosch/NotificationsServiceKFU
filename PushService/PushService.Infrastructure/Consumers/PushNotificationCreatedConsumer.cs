using Core.Domain.Enums;
using Core.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using PushService.Application.Interfaces;

namespace PushService.Infrastructure.Consumers;

public class PushNotificationCreatedConsumer(
    IPushNotificationService pushService,
    ILogger<PushNotificationCreatedConsumer> logger) 
    : IConsumer<NotificationCreatedEvent>
{
    public async Task Consume(ConsumeContext<NotificationCreatedEvent> context)
    {
        var message = context.Message;
        if (message.Type != NotificationType.Push) return;

        try 
        {
            await pushService.SendAsync(message.RecipientContact, message.Content, context.CancellationToken);

            await context.Publish(new NotificationSentEvent(message.Id, DateTime.UtcNow));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to process Push");
            throw;
        }
    }
}