using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Encounters.API.Dtos;
using System.Xml.Linq;
using AutoMapper;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterRepository : CrudDatabaseRepository<Encounter, EncountersContext>, IEncounterRepository
    {
        private readonly EncountersContext _dbContext;
        private readonly IMapper _mapper;

        public EncounterRepository(EncountersContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<Encounter> GetAll()
        {
            return _dbContext.Encounters.ToList();
        }

        public Encounter GetById(long id)
        {
            return _dbContext.Encounters
             .Where(enc => enc.Id == id)
             .FirstOrDefault();

        }
        
        public EncounterDto Create(EncounterDto encounterDto)
        {
            try
            {
                // Koristi AutoMapper za mapiranje DTO-a na odgovarajući entitet
                Encounter encounter;

                if (encounterDto.Type == API.Dtos.EncounterType.SOCIAL)
                {
                    encounter = _mapper.Map<SocialEncounter>(encounterDto);
                    _dbContext.SocialEncounters.Add((SocialEncounter)encounter);
                }
                else if (encounterDto.Type == API.Dtos.EncounterType.HIDDENLOCATION)
                {
                    encounter = _mapper.Map<HiddenLocationEncounter>(encounterDto);
                    _dbContext.HiddenLocationEncounters.Add((HiddenLocationEncounter)encounter);
                }
                else if (encounterDto.Type == API.Dtos.EncounterType.MISC)
                {
                    encounter = _mapper.Map<MiscEncounter>(encounterDto);
                    _dbContext.MiscEncounters.Add((MiscEncounter)encounter);
                }
                else
                {
                    encounter = _mapper.Map<Encounter>(encounterDto);
                    _dbContext.Encounters.Add(encounter);
                }

                _dbContext.SaveChanges();

                // Vratimo mapirani DTO
                return _mapper.Map<EncounterDto>(encounter);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating encounter", ex);
            }
        }



        public EncounterDto Update(EncounterDto encounterDto) {

            try
            {
                // Koristi AutoMapper za mapiranje DTO-a na odgovarajući entitet
                Encounter encounter;

                if (encounterDto.Type == API.Dtos.EncounterType.SOCIAL)
                {
                    encounter = _mapper.Map<SocialEncounter>(encounterDto);
                    _dbContext.SocialEncounters.Update((SocialEncounter)encounter);
                }
                else if (encounterDto.Type == API.Dtos.EncounterType.HIDDENLOCATION)
                {
                    encounter = _mapper.Map<HiddenLocationEncounter>(encounterDto);
                    _dbContext.HiddenLocationEncounters.Update((HiddenLocationEncounter)encounter);
                }
                else if (encounterDto.Type == API.Dtos.EncounterType.MISC)
                {
                    encounter = _mapper.Map<MiscEncounter>(encounterDto);
                    _dbContext.MiscEncounters.Update((MiscEncounter)encounter);
                }
                else
                {
                    encounter = _mapper.Map<Encounter>(encounterDto);
                    _dbContext.Encounters.Update(encounter);
                }

                _dbContext.SaveChanges();

                // Vratimo mapirani DTO
                return _mapper.Map<EncounterDto>(encounter);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating encounter", ex);
            }
        }


       

    }
}
