using Core.Domain.Enums;

namespace Gateway.Api.Contracts;

public record CreateNotificationRequest(
    NotificationType Type,
    string RecipientContact,
    string? Subject,
    string Content);