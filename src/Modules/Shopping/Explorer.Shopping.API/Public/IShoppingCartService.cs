using Explorer.Shopping.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Shopping.API.Public
{
    public interface IShoppingCartService
    {
        Result<ShoppingCartDto> AddItem(OrderItemDto itemDto, int userId);
        Result<ShoppingCartDto> RemoveItem(OrderItemDto itemDto, int userId);
        Result<ShoppingCartDto> CheckOut(long userId, string? couponCode);
    }
}
