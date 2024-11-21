using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface IMiscEncounterService
    {

        Result<MiscEncounterDto> Create(MiscEncounterDto encounter);

        Result<MiscEncounterDto> Update(MiscEncounterDto encounter);

        Result<MiscEncounterDto> GetById(long id);

        Result<PagedResult<MiscEncounterDto>> GetPaged(int page, int pageSize);
    }   
}
