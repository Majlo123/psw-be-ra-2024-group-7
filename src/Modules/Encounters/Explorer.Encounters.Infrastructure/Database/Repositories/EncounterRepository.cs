using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories
{
    public class EncounterRepository : CrudDatabaseRepository<Encounter, EncountersContext>, IEncounterRepository
    {
        private readonly EncountersContext _dbContext;

        public EncounterRepository(EncountersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
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

        public Encounter Create(Encounter encounter)
        {

            try
            {
                _dbContext.Encounters.Add(encounter);
                _dbContext.SaveChanges();

                return encounter;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

    

        public Encounter Update(Encounter encounter) {

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
