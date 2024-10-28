using Explorer.BuildingBlocks.Core.UseCases;
using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.TourProblemReports;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TourProblemReportService : CrudService<TourProblemReportDto, TourProblemReport>, ITourProblemReportService
    {
        private readonly ITourProblemReportRepository _tourProblemReportRepository;
        public TourProblemReportService(ICrudRepository<TourProblemReport> repository, IMapper mapper, ITourProblemReportRepository tourProblemReportRepository) : base(repository, mapper)
        {
            _tourProblemReportRepository = tourProblemReportRepository;
        }

        public Result<PagedResult<TourProblemReportDto>> GetByTouristId(int id, int page, int pageSize)
        {
            var result = _tourProblemReportRepository.GetByTouristId(id, page, pageSize);
            return MapToDto(result);
        }

    }
}
