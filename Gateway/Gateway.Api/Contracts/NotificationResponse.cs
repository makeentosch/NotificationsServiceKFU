using Core.Domain.Enums;

namespace Gateway.Api.Contracts;

public record NotificationResponse(
    Guid Id,
    NotificationStatus Status,
    NotificationType Type,
    string RecipientContact,
    string? Subject,
    string Content,
    DateTime CreatedAt);