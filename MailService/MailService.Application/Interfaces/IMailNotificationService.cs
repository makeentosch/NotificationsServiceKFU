namespace MailService.Application.Interfaces;

public interface IMailNotificationService
{
    Task SendAsync(string recipientContact, string text, CancellationToken cancellationToken);
}