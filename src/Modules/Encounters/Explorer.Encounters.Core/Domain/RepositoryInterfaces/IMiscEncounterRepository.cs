using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IMiscEncounterRepository
    {

        MiscEncounter Create(MiscEncounter encounter);

        MiscEncounter Update(MiscEncounter encounter);

        MiscEncounter GetById(long id);

        PagedResult<MiscEncounter> GetPaged(int page, int pageSize);
    }
}
