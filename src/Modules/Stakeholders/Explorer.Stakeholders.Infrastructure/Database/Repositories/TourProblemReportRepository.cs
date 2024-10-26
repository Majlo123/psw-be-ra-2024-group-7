using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.TourProblemReports;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class TourProblemReportRepository : CrudDatabaseRepository<TourProblemReport, StakeholdersContext>, ITourProblemReportRepository
    {
        private readonly StakeholdersContext _context;

        public TourProblemReportRepository(StakeholdersContext context) : base(context) { }

        public new TourProblemReport? Get(int id)
        {
            return DbContext.TourProblemReports.Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public new TourProblemReport Update(TourProblemReport tourProblemReport)
        {
            DbContext.Entry(tourProblemReport).State = EntityState.Modified;
            DbContext.SaveChanges();
            return tourProblemReport;
        }

        // Get a list of Tour Problems by their Priority
        public List<TourProblemReport> GetByPriority(ProblemPriority priority)
        {
            return _context.TourProblemReports.Where(tpr => tpr.Priority == priority).ToList();
        }
    }
}
