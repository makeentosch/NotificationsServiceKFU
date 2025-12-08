namespace Core.Domain.Events;

public record NotificationSentEvent(
    Guid NotificationId,
    DateTime SentAt
);