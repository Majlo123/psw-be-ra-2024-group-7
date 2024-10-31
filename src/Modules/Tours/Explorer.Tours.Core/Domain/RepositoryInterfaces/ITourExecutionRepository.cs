using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.TourExecutions;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourExecutionRepository
    {
        PagedResult<TourExecution> GetPaged(int page, int pageSize);
        TourExecution Get(int id);
        Result Create(TourExecution execution);
        TourExecution Update(TourExecution tour);

        TourExecution GetByUserAndTourIds(int touristId, int tourId);





    }
}
