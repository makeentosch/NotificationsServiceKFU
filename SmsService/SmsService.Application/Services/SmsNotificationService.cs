using SmsService.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace SmsService.Application.Services;

public class SmsNotificationService(ISmsSender sender, ILogger<SmsNotificationService> logger)
    : ISmsNotificationService
{
    public async Task SendAsync(string recipientContact, string text, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(recipientContact))
        {
            logger.LogWarning("Empty recipient contact");
            return;
        }

        logger.LogInformation("Application layer: Delegating to sender...");

        await sender.SendSmsAsync(recipientContact, text, cancellationToken);
    }
}