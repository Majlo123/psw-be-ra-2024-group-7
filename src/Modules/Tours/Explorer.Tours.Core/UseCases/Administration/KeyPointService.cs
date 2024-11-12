using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration;

public class KeyPointService : BaseService<KeyPointDto, KeyPoint>, IKeyPointService
{
    private readonly IKeyPointRepository _keyPointRepository;
    public KeyPointService(IKeyPointRepository keyPointRepository, IMapper mapper) : base(mapper) 
    {
        _keyPointRepository = keyPointRepository;
    }

    public Result<KeyPointDto> Create(KeyPointDto keyPoint)
    {
        try
        {
            if (!_keyPointRepository.DoesExistByCoordinates(keyPoint.Longitude, keyPoint.Latitude))
            {
                var result = _keyPointRepository.Create(MapToDomain(keyPoint));
                return MapToDto(result);
            }
            else
            {
               return Result.Fail(FailureCode.InvalidArgument).WithError("Taj keypoint vec postoji!");
            }
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<List<KeyPointDto>> GetAll()
    {
        try 
        {
            var result = _keyPointRepository.GetAll();
            return MapToDto(result);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<KeyPointDto> Update(KeyPointDto keyPoint)
    {
        try
        {
            var result = _keyPointRepository.Update(MapToDomain(keyPoint));
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
