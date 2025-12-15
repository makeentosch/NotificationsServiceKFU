using Core.Domain.Enums;
using Core.Domain.Events;
using MailService.Application.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace MailService.Infrastructure.Consumers;

public class MailNotificationCreatedConsumer(
    IMailSender mailSender,
    ILogger<MailNotificationCreatedConsumer> logger)
    : IConsumer<NotificationCreatedEvent>
{
    public async Task Consume(ConsumeContext<NotificationCreatedEvent> context)
    {
        var message = context.Message;

        if (message.Type != NotificationType.Email) return;

        logger.LogInformation("MailService got the task to send a message id={MessageId} to {Recipient}.", message.Id,
            message.RecipientContact);

        try
        {
            await mailSender.SendMailAsync(message.RecipientContact, message.Content, context.CancellationToken);
            await context.Publish(new NotificationSentEvent(message.Id, DateTime.UtcNow));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Message id={MessageId} could not be send.", message.Id);
            throw;
        }
    }
}