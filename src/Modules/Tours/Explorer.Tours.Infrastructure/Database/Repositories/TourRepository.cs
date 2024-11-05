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
using Explorer.Tours.API.Dtos;
using Npgsql;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly ToursContext _context;
        private readonly IKeyPointRepository _keyPointRepository;

        public TourRepository(ToursContext context, IKeyPointRepository keyPointRepository)
        {
            _context = context;
            _keyPointRepository = keyPointRepository;
        }
        public void DeleteEquipmenmts(long id)
        {
            string sqlScript = @"
            DELETE FROM tours.""EquipmentTour""
            WHERE ""TourId"" = @id";
            _context.Database.ExecuteSqlRaw(sqlScript, new NpgsqlParameter("@id", id));
        }

        public void Delete(long id)
        {
            var tour = Get(id);

            if (tour == null)
                throw new KeyNotFoundException();

            var keyPointIds = tour.KeyPoints.Select(k => k.Id).ToList();

            foreach (var keyPointId in keyPointIds)
            {
                _keyPointRepository.Delete(keyPointId);
            }

            _context.Tours.Remove(tour);
            _context.SaveChanges();
        }


        // Get a specific Tour by ID
        public Tour Get(long id)
        {
            return _context.Tours.Include(t => t.KeyPoints).Include(t => t.Equipments).FirstOrDefault(t => t.Id == id);
        }

        // Get a list of Tours by their Status
        public List<Tour> GetByStatus(int status)
        {
            return _context.Tours.Include(t => t.KeyPoints).Include(t => t.Equipments).Where(t => (int) t.Status == status).ToList();
        }
        public PagedResult<Tour> GetPaged(int page, int pageSize)
        {
            var task = _context.Tours.Include(t => t.KeyPoints).Include(t => t.Equipments).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
            
        }   

        public PagedResult<Tour> GetPublishedTour(int page, int pageSize)
        {
            var task = _context.Tours.Include(t => t.KeyPoints).Where(t => t.Status == Core.Domain.TourStatus.Published).Select(t => t.Preview()).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }
    }
}
