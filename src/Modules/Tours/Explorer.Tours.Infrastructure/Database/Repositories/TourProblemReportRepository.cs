using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourProblemReportRepository : ITourProblemReportRepository
    {
        private readonly ToursContext _context;

        public TourProblemReportRepository(ToursContext context)
        {
            _context = context;
        }

        public TourProblemReport Get(int id)
        {
            return _context.TourProblemReports.FirstOrDefault(tpr => tpr.Id == id);
        }

        // Get a list of Tours by their Status
        public List<TourProblemReport> GetByPriority(ProblemPriority priority)
        {
            return _context.TourProblemReports.Where(tpr => tpr.Priority == priority).ToList();
        }
    }
}
