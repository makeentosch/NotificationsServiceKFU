namespace PushService.Application.Interfaces;

public interface IPushSender
{
    Task SendPushAsync(string recipientContact, string message, CancellationToken cancellationToken);
}