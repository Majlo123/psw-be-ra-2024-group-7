using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Author;

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
            var result = _keyPointRepository.Create(MapToDomain(keyPoint));
            return MapToDto(result);
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
}
