using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain.TourProblemReports;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface ITourProblemReportRepository
    {
        TourProblemReport Get(int id);
        List<TourProblemReport> GetByPriority(ProblemPriority priority);
    }
}
