using Core.Domain.Enums;
using Core.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using SmsService.Application.Interfaces;

namespace SmsService.Infrastructure.Consumers;

public class SmsNotificationCreatedConsumer(
    ISmsNotificationService smsService,
    ILogger<SmsNotificationCreatedConsumer> logger) 
    : IConsumer<NotificationCreatedEvent>
{
    public async Task Consume(ConsumeContext<NotificationCreatedEvent> context)
    {
        var message = context.Message;
        if (message.Type != NotificationType.Sms) return;

        try 
        {
            await smsService.SendAsync(message.RecipientContact, message.Content, context.CancellationToken);
            await context.Publish(new NotificationSentEvent(message.Id, DateTime.UtcNow));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to process SMS");
            throw;
        }
    }
}