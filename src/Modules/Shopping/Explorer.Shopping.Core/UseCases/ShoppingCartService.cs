using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Shopping.API.Dtos;
using Explorer.Shopping.API.Public;
using Explorer.Shopping.Core.Domain.RepositoryInterfaces;
using Explorer.Shopping.Core.Domain.ShoppingCarts;
using Explorer.Shopping.Core.Domain;
using FluentResults;
using System.Diagnostics.CodeAnalysis;

namespace Explorer.Payments.Core.UseCases;

public class ShoppingCartService : BaseService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
{
    private readonly IMapper _mapper;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IItemRepository _itemRepository;
    private readonly ITourPurchaseTokenRepository _purchaseTokenRepository;
    private readonly IBundleRepository _bundleRepository;
    private readonly IPaymentRecordRepository _paymentRecordRepository;

    public ShoppingCartService(IShoppingCartRepository repository, IItemRepository itemRepository, 
        ITourPurchaseTokenRepository purchaseTokenRepository, IBundleRepository bundleRepository, 
        IPaymentRecordRepository paymentRecordRepository, IMapper mapper) : base(mapper)
    {
        _mapper = mapper;
        _shoppingCartRepository = repository;
        _itemRepository = itemRepository;
        _purchaseTokenRepository = purchaseTokenRepository;
        _bundleRepository = bundleRepository;
        _paymentRecordRepository = paymentRecordRepository;
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

    public Result<ShoppingCartDto> CheckOut(long userId)
    {
        try
        {
            var shoppingCart = _shoppingCartRepository.GetByUser(userId);
            if (shoppingCart.IsEmpty()) throw new ArgumentException("Can't proceed, shopping cart is empty!");


            UpdateShoppingCart(shoppingCart, true);

            var purchasedItems = GetPurchasedItems(shoppingCart);

            foreach(var item in purchasedItems)
            {
                if(item.Type == 0)
                {
                    _purchaseTokenRepository.Create(new TourPurchaseToken(userId, item.ItemId));
                }
                else
                {
                    var tours = GetPurchasedTours(userId);
                    Bundle bundle = _bundleRepository.GetById(item.ItemId);
                    foreach(var tour in bundle.Products)
                    {
                        if (tours.Value.Where(t => t.ItemId == tour.TourId).Count() != 0)
                            continue;
                        _purchaseTokenRepository.Create(new TourPurchaseToken(userId, tour.TourId));
                    }
                }
                _paymentRecordRepository.Create(new PaymentRecord(userId, item.ItemId, item.Price, DateTime.UtcNow));
            }
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
    public Result<List<ItemDto>> GetPurchasedTours(long userId)
    {
        var purchasedTourIds = _purchaseTokenRepository.GetByUser(userId)
            .Select(tp => tp.TourId)
            .ToList();

        var items = _itemRepository.GetItemsByTourIds(purchasedTourIds);
        return Result.Ok(items.Select(item => _mapper.Map<ItemDto>(item)).ToList());
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

    public Result<ShoppingCartDto> CreateCart(int touristId)
    {
        ShoppingCart shoppingCart = new ShoppingCart(touristId);
        return MapToDto(_shoppingCartRepository.Create(shoppingCart));
    }

    public Result<ItemDto> CreateItem(ItemDto item)
    {
        Item newItem = _mapper.Map<ItemDto, Item>(item);
        return _mapper.Map < Item,ItemDto>(_itemRepository.Create(newItem));
    }

    public Result<ItemDto> GetById(int id)
    {
        if(_itemRepository.GetByItemId(id) == null)
        {
            return null;
        }
        return _mapper.Map < Item,ItemDto >(_itemRepository.GetByItemId(id));
    }

    public Result<ItemDto> UpdateItemByTourId(int tourId,int price)
    {
        Item newItem = _itemRepository.GetByItemId(tourId);
        newItem.Price = price;
        return _mapper.Map<Item, ItemDto>(_itemRepository.Update(newItem));

    }
}

