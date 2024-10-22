using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class TourProblemReportRepository : ITourProblemReportRepository
    {
        private readonly StakeholdersContext _context;

        public TourProblemReportRepository(StakeholdersContext context)
        {
            _context = context;
        }

        public TourProblemReport Get(int id)
        {
            return _context.TourProblemReports.FirstOrDefault(tpr => tpr.Id == id);
        }

        // Get a list of Tour Problems by their Priority
        public List<TourProblemReport> GetByPriority(ProblemPriority priority)
        {
            return _context.TourProblemReports.Where(tpr => tpr.Priority == priority).ToList();
        }
    }
}
