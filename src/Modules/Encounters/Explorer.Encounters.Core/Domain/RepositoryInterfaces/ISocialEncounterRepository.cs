using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface ISocialEncounterRepository
    {

        SocialEncounter Create(SocialEncounter encounter);

        SocialEncounter Update(SocialEncounter encounter);

        SocialEncounter GetById(long id);

        PagedResult<SocialEncounter> GetPaged(int page, int pageSize);
    }
}
