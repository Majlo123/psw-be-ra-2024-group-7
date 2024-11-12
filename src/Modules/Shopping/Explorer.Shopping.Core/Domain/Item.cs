using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Shopping.Core.Domain
{
    public class Item:Entity
    {
        public long SellerId { get; init; }
        public long ItemId { get; init; }
        public string Name { get; private set; }
        public int Price { get; set; }

        public Item(long sellerId, long itemId, string name, int price)
        {
            SellerId = sellerId;
            ItemId = itemId;
            Name = name;
            Price = price;
            Validate();
        }
        public Item(Item item)
        {
            Id = item.Id;
            SellerId = item.SellerId;
            ItemId = item.ItemId;
            Name = item.Name;
            Price = item.Price;
        }

        private void Validate()
        {
            if (SellerId == 0) throw new ArgumentException("Invalid SellerId");
            if (ItemId == 0) throw new ArgumentException("Invalid ItemId");
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name.");
            if (Price < 0) throw new ArgumentException("Invalid Price.");
        }
    }
}
