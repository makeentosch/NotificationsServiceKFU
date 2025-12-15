using MailService.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace MailService.Application.Services;

public class MailNotificationService(IMailSender sender, ILogger<MailNotificationService> logger)
    : IMailNotificationService
{
    public async Task SendAsync(string recipientContact, string text, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(recipientContact))
        {
            logger.LogWarning("Empty recipient contact");
            return;
        }

        logger.LogInformation("Application layer: Delegating to sender...");

        await sender.SendMailAsync(recipientContact, text, cancellationToken);
    }
}