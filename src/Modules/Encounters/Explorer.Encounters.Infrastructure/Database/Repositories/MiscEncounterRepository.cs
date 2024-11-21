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
//  //  public class MiscEncounterRepository : CrudDatabaseRepository<MiscEncounter, EncountersContext>, IMiscEncounterRepository
//    {
//        //public EncountersContext _dbContext;
//        //public MiscEncounterRepository(EncountersContext dbContext) : base(dbContext)
//        //{
//        //    _dbContext = dbContext;
//        //}

//        //public MiscEncounter GetById(long id)
//        //{
//        //    return _dbContext.MiscEncounters
//        //      .Where(enc => enc.Id == id)
//        //      .FirstOrDefault();
//        //}

//        //public MiscEncounter Create(MiscEncounter encounter)
//        //{
//        //    try
//        //    {
//        //        _dbContext.MiscEncounters.Add(encounter);
//        //        _dbContext.SaveChanges();

//        //        return encounter;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw new Exception();
//        //    }
//        //}
//        //public MiscEncounter Update(MiscEncounter encounter)
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
