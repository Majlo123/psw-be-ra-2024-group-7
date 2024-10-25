using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Shopping.Core.Domain.ShoppingCarts
{
    public class ShoppingCart : Entity
    {
        public long UserId { get; init; }
        public List<OrderItem> Items { get; private set; } = new List<OrderItem>();
        public decimal TotalPrice { get; private set; }
        public List<TourPurchaseToken> TourPurchaseTokens { get; private set; } = new List<TourPurchaseToken>();

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
            CalculateTotalPrice();
        }

        public void RemoveItem(OrderItem item)
        {
            Items.Remove(item);
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = Items.Sum(i => i.Price);
        }

        public void Checkout()
        {
            foreach (var item in Items)
            {
                TourPurchaseTokens.Add(new TourPurchaseToken(UserId, item.ItemId));
            }
        }
    }

}
