using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Shopping.Core.Domain.RepositoryInterfaces;
using Explorer.Shopping.Core.Domain.ShoppingCarts;
using Explorer.Shopping.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Shopping.Infrastructure.Database.Repository
{
    public class ShoppingCartRepository : CrudDatabaseRepository<ShoppingCart, ShoppingContext>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ShoppingContext dbContext) : base(dbContext) {}

        public ShoppingCart GetByUser(long userId)
        {
            var shoppingCart = DbContext.ShoppingCarts.FirstOrDefault(c => c.UserId == userId);
            if (shoppingCart == null) throw new KeyNotFoundException("Not found: " + userId);
            return shoppingCart;
        }

    }
}
