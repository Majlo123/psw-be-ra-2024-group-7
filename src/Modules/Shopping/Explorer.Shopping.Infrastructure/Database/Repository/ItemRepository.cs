using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Shopping.Core.Domain;
using Explorer.Shopping.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Shopping.Infrastructure.Database.Repository
{
    public class ItemRepository : CrudDatabaseRepository<Item, ShoppingContext>, IItemRepository
    {
        public ItemRepository(ShoppingContext dbContext) : base(dbContext)
        {
        }

        public Item GetByItemId(long itemId)
        {
            var item = DbContext.Items.FirstOrDefault(i => i.ItemId== itemId);
            if (item == null) throw new KeyNotFoundException("Not found: " + itemId);
            return item;
        }

        public void DeleteByItemId(long itemId)
        {
            var item = DbContext.Items.FirstOrDefault(i => i.ItemId == itemId);
            if (item == null) throw new KeyNotFoundException("Not found: " + itemId);
            DbContext.Items.Remove(item);
            DbContext.SaveChanges();
        }
    }
}
