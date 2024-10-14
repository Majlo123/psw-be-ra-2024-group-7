
namespace Explorer.Tours.API.Dtos
{
    public enum ProblemPriority
    {
        LOW,
        MEDIUM,
        HIGH
    }
    public class TourProblemReportDto
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public string Category { get; set; }
        public ProblemPriority Priority { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
    }
}
