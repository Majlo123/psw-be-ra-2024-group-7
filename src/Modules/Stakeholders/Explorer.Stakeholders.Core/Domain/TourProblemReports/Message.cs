using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.TourProblemReports
{
    public class Message : ValueObject<Message>
    {
        public int UserId { get; set; }
        public int ReportId { get; set; }
        public string Content { get; set; }

        [JsonConstructor]
        public Message(int userId, int reportId, string content) 
        { 
            UserId = userId;
            ReportId = reportId;
            Content = content;
        }

        protected override bool EqualsCore(Message other)
        {
            return UserId == other.UserId &&
                ReportId == other.ReportId &&
                Content == other.Content;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = UserId.GetHashCode();
                hashCode = (hashCode * 397) ^ ReportId.GetHashCode();
                hashCode = (hashCode * 397) ^ Content.GetHashCode();
                return hashCode;
            }
        }
    }
}
