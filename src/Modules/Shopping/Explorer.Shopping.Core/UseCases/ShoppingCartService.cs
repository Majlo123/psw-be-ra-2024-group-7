using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Shopping.API.Dtos;
using Explorer.Shopping.API.Public;
using Explorer.Shopping.Core.Domain.RepositoryInterfaces;
using Explorer.Shopping.Core.Domain.ShoppingCarts;
using Explorer.Shopping.Core.Domain;
using FluentResults;

namespace Explorer.Payments.Core.UseCases;

public class ShoppingCartService : BaseService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
{
    private readonly IMapper _mapper;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IItemRepository _itemRepository;
    private readonly ITourPurchaseTokenRepository _purchaseTokenRepository;

    public ShoppingCartService(IShoppingCartRepository repository, IItemRepository itemRepository,ITourPurchaseTokenRepository purchaseTokenRepository, ICrudRepository<PaymentRecord> paymentRecordRepository,
       IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
        _shoppingCartRepository = repository;
        _itemRepository = itemRepository;
        _purchaseTokenRepository = purchaseTokenRepository;
    }

    public Result<ShoppingCartDto> GetByUser(long userId)
    {
        try
        {
            var result = _shoppingCartRepository.GetByUser(userId);

            UpdateShoppingCart(result);

            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<ShoppingCartDto> AddItem(ItemDto orderItemDto, int userId)
    {
        try
        {
            var cart = _shoppingCartRepository.GetByUser(userId);

            var orderItem = _mapper.Map<ItemDto, OrderItem>(orderItemDto);

            var hasPurchased = _purchaseTokenRepository.HasPurchasedTour(orderItem.ItemId, userId);
            if (hasPurchased) throw new ArgumentException("Tour has already been purchased.");

            _itemRepository.GetByItemId(orderItem.ItemId);

            cart.AddItem(orderItem);

            var result = _shoppingCartRepository.Update(cart);

            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<ShoppingCartDto> RemoveItem(ItemDto orderItemDto, int userId)
    {
        try
        {
            var cart = _shoppingCartRepository.GetByUser(userId);

            var orderItem = _mapper.Map<ItemDto, OrderItem>(orderItemDto);
            cart.RemoveItem(orderItem);

            var result = _shoppingCartRepository.Update(cart);

            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<ShoppingCartDto> CheckOut(long userId, string? couponCode)
    {
        try
        {
            var shoppingCart = _shoppingCartRepository.GetByUser(userId);
            if (shoppingCart.IsEmpty()) throw new ArgumentException("Can't proceed, shopping cart is empty!");


            UpdateShoppingCart(shoppingCart, true);

            var purchasedItems = GetPurchasedItems(shoppingCart);
            CreatePurchaseTokens(userId, purchasedItems);

            shoppingCart.Checkout();
            var result = _shoppingCartRepository.Update(shoppingCart);

            return MapToDto(result);
        }
        catch (KeyNotFoundException e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
    private List<Item> GetPurchasedItems(ShoppingCart shoppingCart)
    {
        var purchasedItems = new List<Item>();
        foreach (var orderItem in shoppingCart.Items)
        {
            var item = _itemRepository.GetByItemId(orderItem.ItemId);
            purchasedItems.Add(new Item(item));
        }

        return purchasedItems;
    }

    private void UpdateShoppingCart(ShoppingCart shoppingCart, bool isCheckout = false)
    {
        var items = shoppingCart.Items.ToList();
        foreach (var orderItem in items)
        {
            var updatedItem = _itemRepository.GetByItemId(orderItem.ItemId);

            if (isCheckout && orderItem.Price != updatedItem.Price)
                throw new ArgumentException(
                    $"Pricing mismatch for item {orderItem.Name}: Cart price: {orderItem.Price}, Current price: {updatedItem.Price}");

            shoppingCart.UpdateItem(orderItem, updatedItem);
        }

        _shoppingCartRepository.Update(shoppingCart);
    }
    private void CreatePurchaseTokens(long userId, List<Item> purchasedItems)
    {

        var purchasedTourIds = GetPurchasedTourIds(purchasedItems);
        foreach (var tourId in purchasedTourIds)
        {
            var purchaseToken = new TourPurchaseToken(userId, tourId);
            _purchaseTokenRepository.Create(purchaseToken);
        }
    }

    private List<long> GetPurchasedTourIds(List<Item> purchasedItems)
    {
        var purchasedTourIds = new List<long>();
        foreach (var item in purchasedItems)
        {
           
                purchasedTourIds.Add(item.ItemId);
            
        }

        return purchasedTourIds;
    }
}

