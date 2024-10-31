using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Shopping.API.Dtos;
using Explorer.Shopping.API.Public;
using Explorer.Stakeholders.Infrastructure.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Shopping;

[Authorize(Policy = "touristPolicy")]
[Route("api/shopping/shopping-cart")]
public class ShoppingCartController : BaseApiController
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet("{touristId:int}")]
    public ActionResult<ShoppingCartDto> GetByUser(int touristId)
    {
        var result = _shoppingCartService.GetByUser(touristId);
        return CreateResponse(result);
    }
    [HttpGet("purchased/{touristId:int}")]
    public ActionResult<ShoppingCartDto> GetPurchasedTours(int touristId)
    {
        var result = _shoppingCartService.GetPurchasedTours(touristId);
        return CreateResponse(result);
    }
    [HttpPut("add/{touristId:int}")]
    public ActionResult<ShoppingCartDto> AddItem([FromBody] ItemDto orderItem, int touristId)
    {
        var result = _shoppingCartService.AddItem(orderItem, touristId);
        return CreateResponse(result);
    }

    [HttpPut("remove/{touristId:int}")]
    public ActionResult<ShoppingCartDto> RemoveItem([FromBody] ItemDto orderItem, int touristId)
    {
        var result = _shoppingCartService.RemoveItem(orderItem, touristId);
        return CreateResponse(result);
    }
    [HttpPut("checkout/{touristId:int}")]
    public ActionResult<ShoppingCartDto> Checkout(int touristId)
    {
        var result = _shoppingCartService.CheckOut(touristId);
        return CreateResponse(result);
    }
}