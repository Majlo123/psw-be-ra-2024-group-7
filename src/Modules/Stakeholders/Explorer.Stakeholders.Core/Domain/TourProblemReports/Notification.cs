using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.TourProblemReports
{
    public enum NotificationType
    {
        CHAT,
        DEADLINE
    }
    public class Notification : ValueObject<Notification>
    {
        public int SenderId { get; private set; }
        public int RecipientId { get; private set; }
        public bool IsRead { get; private set; }
        public NotificationType NotificationType { get; private set; }
        public string Content { get; private set; }

        public Notification() { }

        [JsonConstructor]
        public Notification(int senderId, int recipientId, bool isRead, NotificationType notificationType, string content) 
        { 
            SenderId = senderId;
            RecipientId = recipientId;
            IsRead = isRead;
            NotificationType = notificationType;
            Content = content;
        }

        protected override bool EqualsCore(Notification other)
        {
            return SenderId == other.SenderId &&
            RecipientId == other.RecipientId &&
            IsRead == other.IsRead &&
            NotificationType == other.NotificationType &&
            Content == other.Content;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = SenderId.GetHashCode();
                hashCode = (hashCode * 397) ^ RecipientId.GetHashCode();
                hashCode = (hashCode * 397) ^ IsRead.GetHashCode();
                hashCode = (hashCode * 397) ^ NotificationType.GetHashCode();
                hashCode = (hashCode * 397) ^ Content.GetHashCode();
                return hashCode;
            }
        }
    }
}
