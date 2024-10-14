using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Infrastructure.Database.Repositories;

public class KeyPointRepository : IKeyPointRepository
{
    private readonly ToursContext _dbContext;
    private readonly DbSet<KeyPoint> _dbSet;

    public KeyPointRepository(ToursContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<KeyPoint>();
    }

    public KeyPoint Create(KeyPoint keyPoint)
    {
        _dbSet.Add(keyPoint);
        _dbContext.SaveChanges();
        return keyPoint;
    }

    public List<KeyPoint> GetAll()
    {
        return _dbSet.ToList();
    }
}
