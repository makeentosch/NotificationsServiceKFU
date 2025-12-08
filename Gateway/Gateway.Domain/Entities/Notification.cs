using Core.Domain.Base;
using Core.Domain.Enums;

namespace Gateway.Domain.Entities;

public class Notification : BaseEntity<Guid>
{
    public NotificationType Type { get; set; }

    public string RecipientContact { get; set; } = null!;

    public string? Subject { get; set; }

    public string Content { get; set; } = null!;

    public Guid? SenderId { get; set; }

    public Guid? RecipientId { get; set; }

    public NotificationStatus Status { get; set; }

    protected Notification()
    {
    }

    public Notification(
        Guid id,
        NotificationType type,
        string recipientContact,
        string content,
        string? subject = null,
        Guid? senderId = null,
        Guid? recipientId = null)
        : base(id)
    {
        Type = type;
        Status = NotificationStatus.Created;
        RecipientContact = recipientContact;
        Content = content;
        Subject = subject;
        SenderId = senderId;
        RecipientId = recipientId;
    }
}