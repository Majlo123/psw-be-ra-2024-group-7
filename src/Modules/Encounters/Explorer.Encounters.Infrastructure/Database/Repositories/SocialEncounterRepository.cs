//using Explorer.BuildingBlocks.Infrastructure.Database;
//using Explorer.Encounters.Core.Domain;
//using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Explorer.Encounters.Infrastructure.Database.Repositories
//{
//   // public class SocialEncounterRepository : CrudDatabaseRepository<SocialEncounter, EncountersContext>, ISocialEncounterRepository
//    {
//        //public EncountersContext _dbContext;
//        //public SocialEncounterRepository(EncountersContext dbContext) : base(dbContext)
//        //{
//        //    _dbContext = dbContext;
//        //}

//        //public SocialEncounter GetById(long id)
//        //{
//        //    return _dbContext.SocialEncounters
//        //      .Where(enc => enc.Id == id)
//        //      .FirstOrDefault();
//        //}

//        //public SocialEncounter Create(SocialEncounter encounter)
//        //{
//        //    try
//        //    {
//        //        _dbContext.SocialEncounters.Add(encounter);
//        //        _dbContext.SaveChanges();

//        //        return encounter;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw new Exception();
//        //    }
//        //}
//        //public SocialEncounter Update(SocialEncounter encounter)
//        //{
//        //    try
//        //    {
//        //        _dbContext.Update(encounter);
//        //        _dbContext.SaveChanges();

//        //        return encounter;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw new Exception();
//        //    }
//        //}
//    }
//}
