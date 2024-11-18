using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class HiddenEncounterRepository : CrudDatabaseRepository<HiddenLocationEncounter,EncountersContext>, IHiddenEncounterRepository
    {
        private readonly EncountersContext _dbContext;

        public HiddenEncounterRepository(EncountersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public HiddenLocationEncounter GetById(long id)
        {
            return _dbContext.HiddenLocationEncounters
              .Where(enc => enc.Id == id)
              .FirstOrDefault();
        }

        public HiddenLocationEncounter Create(HiddenLocationEncounter encounter)
        {
            try
            {
                _dbContext.HiddenLocationEncounters.Add(encounter);
                _dbContext.SaveChanges();

                return encounter;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public HiddenLocationEncounter Update(HiddenLocationEncounter encounter)
        {
            try
            {
                _dbContext.Update(encounter);
                _dbContext.SaveChanges();

                return encounter;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

    }
}
