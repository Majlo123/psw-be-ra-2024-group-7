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

        public TourRepository(ToursContext context)
        {
            _context = context;
        }
        public void DeleteEquipmenmts(long id)
        {
            string sqlScript = @"
            DELETE FROM tours.""EquipmentTour""
            WHERE ""TourId"" = @id";
            _context.Database.ExecuteSqlRaw(sqlScript, new NpgsqlParameter("@id", id));
        }

        // Get a specific Tour by ID
        public Tour Get(int id)
        {
            return _context.Tours.Include(t => t.KeyPoints).Include(t => t.Equipments).FirstOrDefault(t => t.Id == id);
        }

        // Get a list of Tours by their Status
        public List<Tour> GetByStatus(string status)
        {
            return _context.Tours.Include(t => t.KeyPoints).Include(t => t.Equipments).Where(t => t.Status == status).ToList();
        }

        public PagedResult<Tour> GetPaged(int page, int pageSize)
        {
            var task = _context.Tours.Include(t => t.KeyPoints).Include(t => t.Equipments).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

 
    }
}
