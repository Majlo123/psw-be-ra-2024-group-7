using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.TourProblemReports
{
    public class Message : ValueObject
    {
        public int UserId { get; private set; }
        public int ReportId { get; private set; }
        public string Content { get; private set; }

        [JsonConstructor]
        public Message(int userId, int reportId, string content) 
        { 
            UserId = userId;
            ReportId = reportId;
            Content = content;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return ReportId;
            yield return Content;
        }
    }
}
