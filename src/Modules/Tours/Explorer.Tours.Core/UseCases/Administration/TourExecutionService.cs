using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourExecutionService : CrudService<TourExecutionDto, TourExecution>, ITourExecutionService
    {
        private readonly ITourExecutionRepository _tourExecutionRepository;
        public TourExecutionService(IMapper mapper, ITourExecutionRepository tourExecutionRepository)  : base(mapper)
        {
            _tourExecutionRepository = tourExecutionRepository;
        }

        public Result<TourExecutionDto> Get(int id)
        {
            var result = _tourExecutionRepository.Get(id);
            return MapToDto(result);
        }

        public Result<TourExecutionDto> GetByUserAndTourIds(int touristId, int tourId)
        {
            try
            {
                var result = _tourExecutionRepository.GetByUserAndTourIds(touristId, tourId);

                return MapToDto(result);
            }
            catch(KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<PagedResult<TourExecutionDto>> GetPaged(int page, int pageSize)
        {
            var result = _tourExecutionRepository.GetPaged(page, pageSize);
            return MapToDto(result);
        }

        public Result<TourExecutionDto> LeaveTour(int touristId,int tourId)
        {
            var tourToLeave = _tourExecutionRepository.GetByUserAndTourIds(touristId, tourId);
            tourToLeave.LeaveTour();

            var result = _tourExecutionRepository.Update(tourToLeave);
            return MapToDto(result);

        }

        public Result StartNewTour(TourExecutionDto tourExecution)
        {
            try
            {
                TourExecution newTour = MapToDomain(tourExecution);
                newTour.StartNewTour();
                var creationResult = _tourExecutionRepository.Create(newTour);

                if (creationResult.IsSuccess)
                {
                    return Result.Ok(); 
                }

                return Result.Fail(creationResult.Errors); 
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<TourExecutionDto> Update(TourExecutionDto tour)
        {
            try
            {
                var result = _tourExecutionRepository.Update(MapToDomain(tour));
                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
