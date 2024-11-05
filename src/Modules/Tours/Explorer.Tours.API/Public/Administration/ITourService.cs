using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITourService
    {
        Result<PagedResult<TourDto>> GetPaged(int page, int pageSize);
        Result<TourDto> Create(TourDto tour);
        Result<TourDto> Update(TourDto tour);
        Result Delete(long id);
        Result<TourDto> Get(long id);
        void DeleteEquipments(long id);
        Result<TourDto> Publish(TourDto tour);
        Result<PagedResult<TourDto>> GetPublishedTour(int page, int pageSize);
        Result<TourDto> Archive(TourDto tour);

        Result<TourDto> UpdateTourLength(TourDto tour);

        Result<TourDto> ReactivateTour(TourDto tour);

    }
}
