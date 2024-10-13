using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Tests.Integration.Administration
{

    [Collection("Sequential")]
    public class TouristClubCommandTests : BaseBlogIntegrationTest
    {
        public TouristClubCommandTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new TouristClubDto
            {
                Name = "TestClub",
                Description = "Test description",
                Picture = "linktoTestPicture",
                OwnerId = 3
            };

            //Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TouristClubDto;

            //Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);

            //Assert - Database
            var storedEntity = dbContext.TouristClub.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TouristClubDto
            {
                Picture = "linktoTestPicture",
                OwnerId = 3
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
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var updatedEntity = new TouristClubDto
            {
                Id = -1,
                Name = "TestUpdateClub",
                Description = "Test update description",
                Picture = "linktoTestUpdatePicture",
                OwnerId = 3
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TouristClubDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Name.ShouldBe(updatedEntity.Name);
            result.Description.ShouldBe(updatedEntity.Description);
            result.Picture.ShouldBe(updatedEntity.Picture);

            // Assert - Database
            var storedEntity = dbContext.TouristClub.FirstOrDefault(i => i.Name == "TestUpdateClub");
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            var oldEntity = dbContext.TouristClub.FirstOrDefault(i => i.Name == "Planinari");
            oldEntity.ShouldBeNull();
        }

       [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TouristClubDto
            {
                Id = -1000,
                Name = "TestUpdateClubFAIL",
                Description = "Test update description FAIL",
                Picture = "linktoTestUpdatePicture FAIL ",
                OwnerId = 3
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        } 


        private static TouristClubController CreateController(IServiceScope scope)
        {
            return new TouristClubController(scope.ServiceProvider.GetRequiredService<IToursitClubService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
