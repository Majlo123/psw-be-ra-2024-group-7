using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.TourProblemReports
{
    public enum Type
    {
        CHAT,
        DEADLINE
    }
    public class Notification : ValueObject
    {
        public int SenderId { get; private set; }
        public int RecipientId { get; private set; }
        public bool IsRead { get; private set; }
        public Type Type { get; private set; }
        public string Content { get; private set; }

        [JsonConstructor]
        public Notification(int senderId, int recipientId, bool isRead, Type type, string content) 
        { 
            SenderId = senderId;
            RecipientId = recipientId;
            IsRead = isRead;
            Type = type;
            Content = content;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SenderId;
            yield return RecipientId;
            yield return IsRead;
            yield return Type;
            yield return Content;
        }
    }
}
