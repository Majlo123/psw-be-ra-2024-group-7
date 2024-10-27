using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourExecutionRepository : CrudDatabaseRepository<TourExecution, ToursContext>, ITourExecutionRepository
    {

        private readonly ToursContext _dbContext;

        public TourExecutionRepository(ToursContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;

        }

        public TourExecution Create(List<Equipment> equipments, List<CompletedKeyPoints> completedKeyPoints)
        {
            TourExecution tourExecution = new TourExecution
            {
                TouristEquipment = equipments,
                CompletedKeyPoints = completedKeyPoints
            };

            _dbContext.TourExecutions.Add(tourExecution);
            _dbContext.SaveChanges();

            return tourExecution;
        }

        public TourExecution Read(long id)
        {
           
            return _dbContext.TourExecutions
                .Where(te => te.Id == id)
                .Include(te => te.TouristEquipment)
                .Include(te => te.CompletedKeyPoints)
                .FirstOrDefault();
        }

        public TourExecution Update(TourExecution aggregateRoot)
        {
            
            _dbContext.Entry(aggregateRoot).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return aggregateRoot;
        }

        public void Delete(TourExecution aggregateRoot)
        {
            
            _dbContext.TourExecutions.Remove(aggregateRoot);
            _dbContext.SaveChanges();
        }


    }
}
