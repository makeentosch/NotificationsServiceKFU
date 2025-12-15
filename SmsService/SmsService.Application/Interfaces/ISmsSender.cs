namespace SmsService.Application.Interfaces;

public interface ISmsSender
{
    Task SendSmsAsync(string recipientContact, string message, CancellationToken cancellationToken);
}