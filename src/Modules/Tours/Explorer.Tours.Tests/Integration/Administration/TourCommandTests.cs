using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.Administration;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class TourCommandTests : BaseToursIntegrationTest
    {
        public TourCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new TourDto
            {
                Name = "Tura4",
                Difficulty = "Laka",
                Description = "Planinski hajk",
                Cost = 200,
                Status = 0,
                Tags = "visina,priroda",
                Length = 0,
                TourDurations = new List<TourDurationDto>(),
                AuthorId = -12
            };

            //Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourDto;

            //Assert - Response

            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);

            //Assert - Database

            var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }
        //Ukoliko se pokusa kreirati nekompletan entitet
        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourDto
            {
                Difficulty = "Laka",
                Description = "Planinski hajk",
                Cost = 0,
                Status = 0,
                Tags = "visina,priroda",
                Length = 0,
                TourDurations = new List<TourDurationDto>(),
                AuthorId = -12

            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Tours.FirstOrDefault(i => i.Id == -3);
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

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new TourDto
            {
                Id = -1,
                Name = "Tura1",
                Difficulty = "Teze",
                Description = "Planinski hajk",
                Cost = 200,
                Status = 0,
                Tags = "visina,priroda",
                TourDurations = new List<TourDurationDto>(),
                Length = 0,
                AuthorId = -12
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Name.ShouldBe(updatedEntity.Name);
            result.Difficulty.ShouldBe(updatedEntity.Difficulty);
            result.Description.ShouldBe(updatedEntity.Description);
            result.Cost.ShouldBe(updatedEntity.Cost);
            result.Tags.ShouldBe(updatedEntity.Tags);

            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Difficulty == "Teze");
            storedEntity.ShouldNotBeNull();
            storedEntity.Difficulty.ShouldBe(updatedEntity.Difficulty);
            var oldEntity = dbContext.Tours.FirstOrDefault(i => i.Id==-1 && i.Difficulty == "Laka");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourDto
            {
                Id = -1000,
                Name = "Tura1"
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static TourController CreateController(IServiceScope scope)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
