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
    public interface ISocialEncounterService
    {

        Result<SocialEncounterDto> Create(SocialEncounterDto encounter);

        Result<SocialEncounterDto> Update(SocialEncounterDto encounter);

        Result<SocialEncounterDto> GetById(long id);

        Result<PagedResult<SocialEncounterDto>> GetPaged(int page, int pageSize);
    }
}
