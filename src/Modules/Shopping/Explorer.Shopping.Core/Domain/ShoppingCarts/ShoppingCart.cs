using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Shopping.Core.Domain.ShoppingCarts
{
    public class ShoppingCart: Entity
    {
        public List<OrderItem> items { get; set; }
        public List<TourPurchaseToken> tourPurchaseTokens { get; set; }
        public ShoppingCart() { }
        public ShoppingCart(List<OrderItem> items, List<TourPurchaseToken> tourPurchaseTokens)
        {
            this.items = items;
            this.tourPurchaseTokens = tourPurchaseTokens;
        }
    }
}
