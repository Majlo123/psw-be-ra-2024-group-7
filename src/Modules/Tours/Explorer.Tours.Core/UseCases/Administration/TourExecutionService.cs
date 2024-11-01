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
        private readonly IMapper _mapper;
        private readonly ITourExecutionRepository _tourExecutionRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IKeyPointRepository _keyPointRepository;
        public TourExecutionService(IMapper mapper, ITourExecutionRepository tourExecutionRepository,ITourRepository tourRepository,IKeyPointRepository keyPointRepository)  : base(mapper)
        {
            _tourExecutionRepository = tourExecutionRepository;
            _mapper = mapper;
            _tourRepository = tourRepository;
            _keyPointRepository = keyPointRepository;
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
        public Result<TourExecutionDto> CheckLocation(int id, double latitude, double longitude)
        {
            try
            {
                var tourExecution = _tourExecutionRepository.Get(id);
                var tour = _tourRepository.Get(tourExecution.TourId);
                tourExecution.UpdateLocation(latitude, longitude);
                //var checkpoint = _tourExecutionRepository.FindNearbyCheckpoint(latitude, longitude,tour);
                foreach (KeyPoint keyPoint in tour.KeyPoints)
                {
                    if (FindNearbyCheckpoint(latitude, longitude, keyPoint.Longitude, keyPoint.Latitude) && tourExecution.CompletedKeyPoints.All(k => k.CompletedKeyPointId != keyPoint.Id) && tourExecution.CompletedKeyPoints.Count>=(keyPoint.Id-1))
                    {
                        CompletedKeyPoints completedKey = new CompletedKeyPoints(DateTime.UtcNow,Convert.ToInt32(keyPoint.Id));
                        tourExecution.AddCompletedKeyPoint(completedKey);
                        tourExecution.UpdateCompletedPercentage(tour.KeyPoints.Count);
                        var result = _tourExecutionRepository.Update(tourExecution);
                        return MapToDto(result);
                    }
                }
                return MapToDto(_tourExecutionRepository.Update(tourExecution));
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

        public Result<List<KeyPointDto>> GetCompletedKeyPoints(int id)
        {
            try
            {
                var tourExecution = _tourExecutionRepository.Get(id);
                var completedKeyPoints = new List<KeyPoint>();
                //var result = tourExecution.CompletedKeyPoints;
                foreach (var completed in tourExecution.CompletedKeyPoints)
                {
                    var keyPoint = _keyPointRepository.Get(completed.CompletedKeyPointId);
                    if (keyPoint != null)
                    {
                        completedKeyPoints.Add(new KeyPoint(keyPoint));
                    }
                }
                var result = completedKeyPoints;
                return Result.Ok(completedKeyPoints.Select(keyPoint => _mapper.Map<KeyPointDto>(keyPoint)).ToList());
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
        private bool FindNearbyCheckpoint(double latitude, double longitude, double keyLongitude, double keyLatitude)
        {

            double distance = CalculateDistance(latitude, longitude, keyLatitude, keyLongitude);
            if (distance <= 20)
            {
                return true;
            }
            return false;
        }
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Haversine formula for distance calculation
            const double R = 6371000; // Radius of the Earth in meters
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);
            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c; // Distance in meters
        }
        private double ToRadians(double angle)
        {
            return angle * Math.PI / 180;
        }
    }
}
