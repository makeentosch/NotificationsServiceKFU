using Core.Domain.Enums;

namespace Core.Domain.Events;

public record NotificationCreatedEvent(
    Guid Id,
    NotificationType Type,
    string RecipientContact,
    string? Subject,
    string Content
);