using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly ToursContext _context;

        public TourRepository(ToursContext context)
        {
            _context = context;
        }

        // Get a specific Tour by ID
        public Tour Get(int id)
        {
            return _context.Tours.FirstOrDefault(t => t.Id == id);
        }

        // Get a list of Tours by their Status
        public List<Tour> GetByStatus(string status)
        {
            return _context.Tours.Where(t => t.Status == status).ToList();
        }
    }
}
