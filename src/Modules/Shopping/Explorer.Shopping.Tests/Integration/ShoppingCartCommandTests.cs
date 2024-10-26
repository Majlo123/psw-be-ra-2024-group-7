using Explorer.API.Controllers.Tourist.Shopping;
using Explorer.Shopping.API.Dtos;
using Explorer.Shopping.API.Public;
using Explorer.Shopping.Tests;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class ShoppingCartCommandTests : BaseShoppingIntegrationTest
    {
        public ShoppingCartCommandTests(ShoppingTestFactory factory) : base(factory) { }

        [Fact]
        public void Adds_item()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ShoppingContext>();

            var item = new ItemDto()
            {
                ItemId = -4,
                Name = "Zimovanje na Tari",
                Price = 200
            };

            // Act
            var result = ((ObjectResult)controller.AddItem(item).Result)?.Value as ShoppingCartDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-3);
            result.UserId.ShouldBe(-23);

            // Assert - Database
            var storedEntity = dbContext.ShoppingCarts.AsEnumerable().Where(c => c.Id == -3 && c.TotalPrice == 250);
            storedEntity.ShouldNotBeNull();
        }

        [Fact]
        public void Removes_item()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ShoppingContext>();

            var item = new ItemDto()
            {
                ItemId = -2,
                Name = "Obilazak beoradskih muzeja",
                Price = 50
            };

            // Act
            var result = ((ObjectResult)controller.RemoveItem(item).Result)?.Value as ShoppingCartDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-3);
            result.UserId.ShouldBe(-23);

            // Assert - Database
            var storedEntity = dbContext.ShoppingCarts.AsEnumerable().Where(c => c.Id == -3 && c.TotalPrice == 0);
            storedEntity.ShouldNotBeNull();
        }


        private static ShoppingCartController CreateController(IServiceScope scope)
        {
            return new ShoppingCartController(scope.ServiceProvider.GetRequiredService<IShoppingCartService>())
            {
                ControllerContext = BuildContext("-21")
            };
        }
    }
}