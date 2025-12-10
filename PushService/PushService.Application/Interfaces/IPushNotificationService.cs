namespace PushService.Application.Interfaces;

public interface IPushNotificationService
{
    Task SendAsync(string recipientContact, string text, CancellationToken cancellationToken);
}