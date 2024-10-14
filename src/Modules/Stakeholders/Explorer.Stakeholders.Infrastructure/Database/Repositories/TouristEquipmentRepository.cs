using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class TouristEquipmentRepository : ITouristEquipmentRepository
    {
        private readonly StakeholdersContext _dbContext;
        public TouristEquipmentRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<TouristEquipment> GetAll(long id)
        {
            return _dbContext.TouristEquipments.Where(t=>t.TouristId==id).ToList();
        }
        public TouristEquipment GetById(int id)
        {
            return _dbContext.TouristEquipments.FirstOrDefault(e => e.Id == id);
        }

    }
}
