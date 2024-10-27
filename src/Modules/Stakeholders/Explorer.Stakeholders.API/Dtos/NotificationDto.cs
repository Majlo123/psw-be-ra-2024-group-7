using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos;
public enum Type
{
    CHAT,
    DEADLINE
}
public class NotificationDto
{
    public int SenderId { get; private set; }
    public int RecipientId { get; private set; }
    public bool IsRead { get; private set; }
    public Type Type { get; private set; }
    public string Content { get; private set; }
}
