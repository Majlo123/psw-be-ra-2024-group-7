using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface ITourProblemReportService
    {
        Result<TourProblemReportDto> Get(int id);
        Result<PagedResult<TourProblemReportDto>> GetPaged(int page, int pageSize);
        Result<PagedResult<TourProblemReportDto>> GetByTouristId(int id, int page, int pageSize);
        PagedResult<TourProblemReportDto> GetByAuthorId(int authorId, int page, int pageSize);
        Result<TourProblemReportDto> Create(TourProblemReportDto tourProblemReport);
        Result<TourProblemReportDto> Update(TourProblemReportDto tourProblemReport);
        Result Delete(int id);

        Result<TourProblemReportDto> SetSolvingDeadline(int id, TourProblemReportDto tourProblemReportDto);

    }
}
