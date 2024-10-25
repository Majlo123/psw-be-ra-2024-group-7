using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Shopping.API.Dtos
{
    public class ShoppingCartDto
    {
        public long UserId { get; init; }
        public List<OrderItemDto> Items { get; private set; } = new List<OrderItemDto>();
        public decimal TotalPrice { get; private set; }
        public List<TourPurchaseTokenDto> TourPurchaseTokens { get; private set; } = new List<TourPurchaseTokenDto>();
    }
}
