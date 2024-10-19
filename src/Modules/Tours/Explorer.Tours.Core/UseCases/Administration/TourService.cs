using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourService : CrudService<TourDto,Tour>, ITourService 
    {
        private readonly ITourRepository _tourRepository;
        public TourService(ICrudRepository<Tour> repository, IMapper mapper, ITourRepository tourRepository) : base (repository, mapper)
        {
            _tourRepository = tourRepository;
        }
        public Result<PagedResult<TourDto>> GetPaged(int page, int pageSize)
        {
           var result = _tourRepository.GetPaged(page, pageSize);
           return MapToDto(result);
        }

        public Result<TourDto> Get(long id)
        {
            var result = _tourRepository.Get(id);
            return MapToDto(result);
        }
        public Result Delete(long id)
        {
            try
            {
                _tourRepository.Delete(id);
                return Result.Ok();
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

    }
}
