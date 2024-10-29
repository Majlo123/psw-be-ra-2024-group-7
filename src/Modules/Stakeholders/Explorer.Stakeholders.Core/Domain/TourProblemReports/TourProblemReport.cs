using Explorer.BuildingBlocks.Core.Domain;
using System.Security.Cryptography;

namespace Explorer.Stakeholders.Core.Domain.TourProblemReports
{
    public enum ProblemPriority
    {
        LOW,
        MEDIUM,
        HIGH
    }

    public enum Status
    {
        UNSOLVED,
        SOLVED,
        CLOSED
    }

    public class TourProblemReport : Entity
    {
        public int TourId { get; private set; }
        public string Category { get; private set; }
        public ProblemPriority Priority { get; private set; }
        public string Description { get; private set; }
        public DateTime Time { get; private set; }
        public Status Status { get; private set; }
        public int TouristId { get; private set; }
        public string? Comment { get; private set; } = "";
        public List<Message> Messages { get; protected set; } = new List<Message>();
        public List<Notification> Notifications { get; protected set; } = new List<Notification>();

        public TourProblemReport(int tourId, string category, ProblemPriority priority, string description, DateTime time, Status status, int touristId, string comment)
        {
            TourId = tourId;
            Category = category;
            Priority = priority;
            Description = description;
            Time = time;
            Status = status;
            TouristId = touristId;
            Comment = comment;
            Validate();
        }

        private void Validate()
        {
            //if (tourId < 0)
            //    throw new ArgumentException("TourId must be 0 or positive number", nameof(tourId));

            if (string.IsNullOrWhiteSpace(Category))
                throw new ArgumentException("Category cannot be empty", nameof(Category));

            if (!Enum.IsDefined(typeof(ProblemPriority), Priority))
                throw new ArgumentException("Invalid priority value", nameof(Priority));

            if (string.IsNullOrWhiteSpace(Description))
                throw new ArgumentException("Description cannot be empty", nameof(Description));

            if (Time > DateTime.Now)
                throw new ArgumentException("Time cannot be in the future", nameof(Time));
        }

        public void AddMessage(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            Messages.Add(message);
        }
    }
}
