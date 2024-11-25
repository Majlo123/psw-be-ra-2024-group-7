using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Shopping.Core.Domain;
using Explorer.Shopping.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Shopping.Infrastructure.Database.Repository;

public class BundleRepository : IBundleRepository
{
    private readonly ShoppingContext _shoppingContext;
    private readonly DbSet<Bundle> _dbSet;

    public BundleRepository(ShoppingContext shoppingContext)
    {
        _shoppingContext = shoppingContext;
        _dbSet = shoppingContext.Set<Bundle>();
    }

    public Bundle Create(Bundle bundle)
    {
        _dbSet.Add(bundle);
        _shoppingContext.SaveChanges();
        return bundle;
    }

    public void Delete(long id)
    {
        var entity = _dbSet.FirstOrDefault(b => b.Id == id);
        _dbSet.Remove(entity);
        _shoppingContext.SaveChanges();
    }

    public Bundle Get(long id)
    {
        return _dbSet.Include(b => b.Products).FirstOrDefault(b => b.Id == id);
    }

    public PagedResult<Bundle> GetPaged(int page, int pageSize)
    {
        var task = _dbSet.Include(b => b.Products).GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }

    public Bundle Update(Bundle bundle)
    {
        _shoppingContext.Update(bundle);
        _shoppingContext.SaveChanges();
        return bundle;
    }

    public PagedResult<Bundle> GetPagedByCreatorId(long creatorId, int page, int pageSize)
    {
        var task = _dbSet.Include(b => b.Products).Where(b => b.CreatorId == creatorId).GetPagedById(page, pageSize);
        task.Wait();
        return task.Result;
    }
}
