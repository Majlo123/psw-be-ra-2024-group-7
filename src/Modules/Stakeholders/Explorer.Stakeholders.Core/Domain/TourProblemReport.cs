using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public enum ProblemPriority
    {
        LOW,    
        MEDIUM, 
        HIGH   
    }
    public class TourProblemReport : Entity
    {
        public int TourId { get; private set; }
        public string Category { get; private set; }
        public ProblemPriority Priority { get; private set; }
        public string Description { get; private set; }
        public DateTime Time { get; private set; }

        public TourProblemReport(int tourId, string category, ProblemPriority priority, string description, DateTime time)
        {
            //if (tourId < 0)
            //    throw new ArgumentException("TourId must be 0 or positive number", nameof(tourId));

            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Category cannot be empty", nameof(category));

            if (!Enum.IsDefined(typeof(ProblemPriority), priority)) 
                throw new ArgumentException("Invalid priority value", nameof(priority));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty", nameof(description));

            if (time > DateTime.Now)
                throw new ArgumentException("Time cannot be in the future", nameof(time));

            TourId = tourId;
            Category = category;
            Priority = priority;
            Description = description;
            Time = time;
        }
    }
}
