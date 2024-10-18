using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourObject = Explorer.Tours.Core.Domain.TourObject;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourObjectRepository : ITourObjectRepository
    {
        private readonly ToursContext _context;

        public TourObjectRepository(ToursContext context)
        {
            _context = context;
        }

        public List<TourObject> GetAll() 
        {
            return _context.TourObjects.ToList();
        }
    }
}
