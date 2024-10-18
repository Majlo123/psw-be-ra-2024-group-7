using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object = Explorer.Tours.Core.Domain.Object;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class ObjectRepository : IObjectRepository
    {
        private readonly ToursContext _context;

        public ObjectRepository(ToursContext context)
        {
            _context = context;
        }

        public List<Object> GetAll() 
        {
            return _context.Objects.ToList();
        }
    }
}
