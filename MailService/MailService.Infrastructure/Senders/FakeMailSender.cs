using MailService.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace MailService.Infrastructure.Senders;

public class FakeMailSender(ILogger<FakeMailSender> logger) : IMailSender
{
    public async Task SendMailAsync(string recipientContact, string message, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);

        logger.LogInformation("Sending Mail to {recipientContact}: {message}", recipientContact, message);
    }
}