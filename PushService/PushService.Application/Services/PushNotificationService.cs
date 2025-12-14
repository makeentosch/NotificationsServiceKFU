using Microsoft.Extensions.Logging;
using PushService.Application.Interfaces;

namespace PushService.Application.Services;

public class PushNotificationService(IPushSender sender, ILogger<PushNotificationService> logger)
    : IPushNotificationService
{
    public async Task SendAsync(string recipientContact, string text, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(recipientContact))
        {
            logger.LogWarning("Empty recipient contact");
            return;
        }

        logger.LogInformation("Application layer: Delegating to sender...");

        await sender.SendPushAsync(recipientContact, text, cancellationToken);
    }
}