using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Author;

public class KeyPointService : BaseService<KeyPointDto, KeyPoint>, IKeyPointService
{
    public KeyPointService(IMapper mapper) : base(mapper) {}

    public Result<KeyPointDto> Create(KeyPointDto keyPoint)
    {
       throw new NotImplementedException(); //trenutno ova metoda ne moze da se implementira jer nije kreiran repozitorijum
    }
}
