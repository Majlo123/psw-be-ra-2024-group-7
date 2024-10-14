using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object = Explorer.Tours.Core.Domain.Object;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class ObjectService : CrudService<ObjectDto, Object>, IObjectService
    {
        public ObjectService(ICrudRepository<Object> repository, IMapper mapper) : base(repository, mapper) { }

    }
}
