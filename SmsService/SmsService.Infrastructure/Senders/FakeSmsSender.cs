using Microsoft.Extensions.Logging;
using SmsService.Application.Interfaces;

namespace SmsService.Infrastructure.Senders;

public class FakeSmsSender(ILogger<FakeSmsSender> logger) : ISmsSender
{
    public async Task SendSmsAsync(string recipientContact, string message, CancellationToken cancellationToken)
    {
        if (message.Contains("FATAL"))
        {
            logger.LogError("Simulating FATAL error for {phone}", recipientContact);
            throw new Exception("Simulated fatal error by user request!");
        }

        await Task.Delay(1000, cancellationToken);

        logger.LogInformation("Sending SMS to {recipientContact}: {message}", recipientContact, message);
    }
}