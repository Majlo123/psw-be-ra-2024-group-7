using Explorer.BuildingBlocks.Core.UseCases;
using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.TourProblemReports;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TourProblemReportService : CrudService<TourProblemReportDto, TourProblemReport>, ITourProblemReportService
    {
        public TourProblemReportService(ICrudRepository<TourProblemReport> repository, IMapper mapper) : base(repository, mapper)
        {
        }

    }
}
