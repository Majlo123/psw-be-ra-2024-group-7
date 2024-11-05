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
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Explorer.Stakeholders.API.Dtos;
using TourStatus = Explorer.Tours.Core.Domain.TourStatus;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourService : CrudService<TourDto,Tour>, ITourService, IInternalTourService
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

        public void DeleteEquipments(long id)
        {
            _tourRepository.DeleteEquipmenmts(id);
        }

        public Result<TourDto> Publish(TourDto tourDto)
        {
            try
            {
                Tour tour = MapToDomain(tourDto);
                tour = tour.Publish();
                return base.Update(MapToDto(tour));
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<TourDto> Archive(TourDto tourDto)
        {
            try
            {
                Tour tour = MapToDomain(tourDto);
                tour = tour.Archive();
                return base.Update(MapToDto(tour));
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<TourDto> UpdateTourLength(TourDto tourDto)
        {
            try
            {
                Tour tour = MapToDomain(tourDto);
                tour = tour.UpdateTourLength(tour.Length);
                return base.Update(MapToDto(tour));
            }
            catch(ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<TourDto> ReactivateTour(TourDto tourDto)
        {
            try
            {
                Tour tour = MapToDomain(tourDto);
                tour = tour.ReactivateTour();
                return base.Update(MapToDto(tour));
            } catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        public Result<List<TourDto>> GetMany(List<long> tourIds)
        {
            var tours = CrudRepository.GetMany(tourIds);
            return MapToDto(tours);
        }

        public Result<PagedResult<TourDto>> GetPublishedTour(int page, int pageSize)
        {
            var result = _tourRepository.GetPublishedTour(page, pageSize);
            return MapToDto(result);
        }
        public Result<TourDto> CloseTour(int id)
        {
            var tour = CrudRepository.Get(id);
            if (tour == null)
            {
                throw new Exception("Tour not found");
            }
            tour.CloseTour();
            CrudRepository.Update(tour);

            return Result.Ok(MapToDto(tour));
        }
        public Result<PagedResult<BasicTourDetailsDto>> GetPublishedTours(int page, int pageSize)
        {
            var result = _tourRepository.GetPublishedTours(page, pageSize);
            var items = result.Results.Select(t => new BasicTourDetailsDto
            {
                Id = t.Id,
                Cost = t.Cost,
                Description = t.Description,
                Name = t.Name,
                Length = t.Length,
                FirstKeyPoint = t.KeyPoints.Any() ? new KeyPointDto
                {
                    Id = (int)t.KeyPoints.First().Id,
                    Name = t.KeyPoints.First().Name,
                    Description = t.KeyPoints.First().Description,
                    Image = t.KeyPoints.First().Image,
                    Latitude = t.KeyPoints.First().Latitude,
                    Longitude = t.KeyPoints.First().Longitude
                } : null 
            }).ToList();

            return new PagedResult<BasicTourDetailsDto>(items, result.TotalCount);
            //return MapToDto(result);
        }
        public Result<BasicTourDetailsDto> GetPublishedTourPreview(long id)
        {
            var result = _tourRepository.Get(id);

            if (result.Status != TourStatus.Published)
                return Result.Fail("Tour is not Public");

            var tourDto = new BasicTourDetailsDto
            {
                Id = result.Id,
                Cost = result.Cost,
                Description = result.Description,
                Name = result.Name,
                Length = result.Length,
                AverageRate = result.getAverageRate(),
                FirstKeyPoint = result.KeyPoints.Any() ? new KeyPointDto
                {
                    Id = (int)result.KeyPoints.First().Id,
                    Name = result.KeyPoints.First().Name,
                    Description = result.KeyPoints.First().Description,
                    Image = result.KeyPoints.First().Image,
                    Latitude = result.KeyPoints.First().Latitude,
                    Longitude = result.KeyPoints.First().Longitude
                } : null
            };

            return Result.Ok(tourDto);
        }
        public Result<TourDto> GetPublishedTourById(long id)
        {
            var result = _tourRepository.Get(id);

            if (result.Status != TourStatus.Published)
                return Result.Fail("Tour is not Public");

            var tourDto = new TourDto
            {
                Id = (int)result.Id,
                Cost = result.Cost,
                Description = result.Description,
                Name = result.Name,
                Length = result.Length,
                AverageRate = result.getAverageRate(),
                KeyPoints = result.KeyPoints.Select(kp => new KeyPointDto
                {
                    Id = (int)kp.Id,
                    Name = kp.Name,
                    Description = kp.Description,
                    Image = kp.Image,
                    Latitude = kp.Latitude,
                    Longitude = kp.Longitude
                }).ToList()
            };

            return tourDto;
        }
    }
}
