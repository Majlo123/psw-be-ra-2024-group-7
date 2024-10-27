
namespace Explorer.Stakeholders.API.Dtos
{
    public enum ProblemPriority
    {
        LOW,
        MEDIUM,
        HIGH
    }
    public enum Status
    {
        SOLVED,
        RESOLVED,
        CLOSED
    }
    public class TourProblemReportDto
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public string Category { get; set; }
        public ProblemPriority Priority { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public Status Status { get;  set; }
        public int TouristId { get;  set; }
        public string Comment { get;  set; }
        public DateTime SolvingDeadline { get; set; }
        public List<MessageDto> Messages { get;  set; } = new List<MessageDto>();
        public List<NotificationDto> Notifications { get;  set; } = new List<NotificationDto>();
    }
}
