using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITourProblemReportService
    {
        Result<PagedResult<TourProblemReportDto>> GetPaged(int page, int pageSize);
        Result<TourProblemReportDto> Create(TourProblemReportDto tourProblemReport);
        Result<TourProblemReportDto> Update(TourProblemReportDto tourProblemReport);
        Result Delete(int id);
    }
}
