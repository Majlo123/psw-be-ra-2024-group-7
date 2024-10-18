using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IObjectService
    {
        Result<PagedResult<ObjectDto>> GetPaged(int page, int pageSize);
        Result<ObjectDto> Create(ObjectDto objectt);
        Result<ObjectDto> Update(ObjectDto objectt);
        Result Delete(int id);
    }
}
