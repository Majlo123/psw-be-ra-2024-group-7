using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IHiddenEncounterRepository
    {
        HiddenLocationEncounter Create(HiddenLocationEncounter encounter);

        HiddenLocationEncounter Update(HiddenLocationEncounter encounter);

        HiddenLocationEncounter GetById(long id);

        PagedResult<HiddenLocationEncounter> GetPaged(int page, int pageSize);

    }
}
