using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Shopping.API.Dtos
{
    public class OrderItemDto
    {
        public long ItemId { get; init; }
        public string Name { get; init; }
        public int Price { get; init; }
    }
}
