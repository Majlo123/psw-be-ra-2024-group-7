using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class TouristLocationCommandTest : BaseStakeholdersIntegrationTest
    {
        public TouristLocationCommandTest(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new TouristLocationDto
            {
                TouristId = 1,
                Latitude = 30.5421F,
                Longitude = 84.9021F,
                IsTourActive = false
            };

            //Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TouristLocationDto;

            //Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TouristId.ShouldBe(1);
            result.Latitude.ShouldBe(30.5421F);
            result.Longitude.ShouldBe(84.9021F);
            result.IsTourActive.ShouldBeFalse();

            //Assert - Database
            var storedEntity = dbContext.TouristLocation.FirstOrDefault(i => i.TouristId == newEntity.TouristId && i.IsTourActive == newEntity.IsTourActive);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TouristLocationDto
            {
                Longitude = 89.84921F
                
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new TouristLocationDto
            {
                Id = -3,
                TouristId = 3,
                Longitude = 11,
                Latitude = 25,
                IsTourActive = true
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TouristLocationDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-3);
            result.TouristId.ShouldBe(updatedEntity.TouristId);
            result.Longitude.ShouldBe(updatedEntity.Longitude);
            result.Latitude.ShouldBe(updatedEntity.Latitude);
            result.IsTourActive.ShouldBe(updatedEntity.IsTourActive);

            // Assert - Database
            var storedEntity = dbContext.TouristLocation.FirstOrDefault(i => i.Longitude == 11);
            storedEntity.ShouldNotBeNull();
            storedEntity.TouristId.ShouldBe(updatedEntity.TouristId);
            storedEntity.Longitude.ShouldBe(updatedEntity.Longitude);
            storedEntity.Latitude.ShouldBe(updatedEntity.Latitude);
            storedEntity.IsTourActive.ShouldBe(updatedEntity.IsTourActive);
            var oldEntity = dbContext.TouristLocation.FirstOrDefault(i => i.Longitude == 20);
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TouristLocationDto
            {
                Id = -1000,
                TouristId = 20,
                Longitude = 80,
                Latitude = 21.421F,
                IsTourActive = false
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = (OkResult)controller.Delete(-1);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.TouristLocation.FirstOrDefault(i => i.Id == -1);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }


        private static TouristLocationController CreateController(IServiceScope scope)
        {
            return new TouristLocationController(scope.ServiceProvider.GetRequiredService<ITouristLocationService>(), scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
