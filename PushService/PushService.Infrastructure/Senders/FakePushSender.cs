using Microsoft.Extensions.Logging;
using PushService.Application.Interfaces;

namespace PushService.Infrastructure.Senders;

public class FakePushSender(ILogger<FakePushSender> logger) : IPushSender
{
    public async Task SendPushAsync(string recipientContact, string message, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);

        logger.LogInformation("Sending PUSH to {recipientContact}: {message}", recipientContact, message);
    }
}