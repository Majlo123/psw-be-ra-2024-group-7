using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos;
public enum NotificationType
{
    CHAT,
    DEADLINE
}
public class NotificationDto
{
    public int SenderId { get; set; }
    public int RecipientId { get; set; }
    public bool IsRead { get; set; }
    public NotificationType NotificationType { get; set; }
    public string Content { get; set; }
}
