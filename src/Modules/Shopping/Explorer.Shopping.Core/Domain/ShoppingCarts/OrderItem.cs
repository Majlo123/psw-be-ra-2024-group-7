using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Shopping.Core.Domain.ShoppingCarts
{
    public class OrderItem : ValueObject<OrderItem>
    {
        public long ItemId { get; init; }
        public string Name { get; init; }
        public int Price { get; init; }

        public OrderItem() { }

        [JsonConstructor]
        public OrderItem(long itemId, string name, int price)
        {
            ItemId = itemId;
            Name = name;
            Price = price;
           
        }

        protected override bool EqualsCore(OrderItem other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}
