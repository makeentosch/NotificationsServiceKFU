using Gateway.Api.Contracts;
using Gateway.Application.Interfaces;
using Gateway.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Api.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> SendNotification(
        [FromBody] CreateNotificationRequest request, 
        CancellationToken cancellationToken)
    {
        var notification = new Notification(
            Guid.NewGuid(),
            request.Type,
            request.RecipientContact,
            request.Content,
            request.Subject
        );
        
        await notificationService.SendNotificationAsync(notification, cancellationToken);
        
        return Accepted(notification.Id);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(NotificationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNotification(Guid id, CancellationToken cancellationToken)
    {
        var notification = await notificationService.GetByIdAsync(id, cancellationToken);

        if (notification is null)
            return NotFound();
        
        var response = MapToResponse(notification);
        
        return Ok(response);
    }
    
    private static NotificationResponse MapToResponse(Notification notification)
    {
        return new NotificationResponse(
            notification.Id,
            notification.Status,
            notification.Type,
            notification.RecipientContact,
            notification.Subject,
            notification.Content,
            notification.CreatedAt
        );
    }
}