using Explorer.BuildingBlocks.Core.Domain;
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

    public void Delete(long id)
    {
        var entity = _dbSet.FirstOrDefault(k => k.Id == id);
        _dbSet.Remove(entity);
        _dbContext.SaveChanges();
    }

    public List<KeyPoint> GetAll()
    {
        return _dbSet.ToList();
    }

    public KeyPoint Update(KeyPoint keyPoint)
    {
        try
        {
            _dbContext.Update(keyPoint);
            _dbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            throw new KeyNotFoundException(e.Message);
        }
        return keyPoint;
    }
}
