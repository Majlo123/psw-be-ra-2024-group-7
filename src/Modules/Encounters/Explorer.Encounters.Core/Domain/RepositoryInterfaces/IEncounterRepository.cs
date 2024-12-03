using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces
{
    public interface IEncounterRepository
    {


        EncounterDto Create(EncounterDto encounter);

        EncounterDto Update(EncounterDto encounter);

        Encounter GetById(long id);

        List<Encounter> GetAll(); 

        List<Encounter> GetTouristRequestEncounters();
        List<Encounter> GetAllByTourId(int tourId);
    }
}
