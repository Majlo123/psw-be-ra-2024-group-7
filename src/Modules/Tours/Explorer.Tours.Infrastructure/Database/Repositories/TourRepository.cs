using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Explorer.BuildingBlocks.Infrastructure.Database;
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
            return _context.Tours.Include(t => t.KeyPoints).FirstOrDefault(t => t.Id == id);
        }

        // Get a list of Tours by their Status
        public List<Tour> GetByStatus(string status)
        {
            return _context.Tours.Include(t => t.KeyPoints).Where(t => t.Status == status).ToList();
        }

        public PagedResult<Tour> GetPaged(int page, int pageSize)
        {
            var task = _context.Tours.Include(t => t.KeyPoints).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
