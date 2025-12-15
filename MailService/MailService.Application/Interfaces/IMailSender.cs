namespace MailService.Application.Interfaces;

public interface IMailSender
{
    Task SendMailAsync(string recipientContact, string message, CancellationToken cancellationToken);
}