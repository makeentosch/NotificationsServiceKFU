namespace SmsService.Application.Interfaces;

public interface ISmsNotificationService
{
    Task SendAsync(string recipientContact, string text, CancellationToken cancellationToken);
}