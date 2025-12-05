namespace Core.Domain.Events;

/// <summary>
/// Событие: Уведомление успешно отправлено провайдеру.
/// </summary>
public record NotificationSentEvent(
    Guid NotificationId,
    DateTime SentAt
);