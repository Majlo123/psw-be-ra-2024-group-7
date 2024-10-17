using Explorer.API.Controllers.Tourist.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Administration
{
    [Collection("Sequential")]
    public class TourProblemReportCommandTests : BaseStakeholdersIntegrationTest
    {
        public TourProblemReportCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new TourProblemReportDto
            {
                TourId = 1, 
                Category = "Tehnički problem", 
                Priority = ProblemPriority.HIGH, 
                Description = "Problem sa internet konekcijom.", 
                Time = DateTime.UtcNow.AddDays(-2)
            };

            //Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourProblemReportDto;

            //Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TourId.ShouldBe(newEntity.TourId);
            result.Category.ShouldBe(newEntity.Category);
            result.Priority.ShouldBe(newEntity.Priority);
            result.Description.ShouldBe(newEntity.Description);
            result.Time.ShouldBe(newEntity.Time); 

            // Assert - Database
            var storedEntity = dbContext.TourProblemReports.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.TourId.ShouldBe(newEntity.TourId);
            storedEntity.Category.ShouldBe(newEntity.Category);
            var storedPriority = (ProblemPriority)storedEntity.Priority;
            storedPriority.ShouldBe(newEntity.Priority);
            storedEntity.Description.ShouldBe(newEntity.Description);
            storedEntity.Time.ShouldBe(newEntity.Time);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourProblemReportDto
            {
                Priority = ProblemPriority.HIGH,
                Description = "Problem sa internet konekcijom.",
                Time = DateTime.Now
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
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = (OkResult)controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.TourProblemReports.FirstOrDefault(i => i.Id == -3);
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
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var updatedEntity = new TourProblemReportDto
            {
                Id = -1, 
                TourId = 1, 
                Category = "Tehnički problem",
                Priority = ProblemPriority.MEDIUM, 
                Description = "Problem sa internet konekcijom.",
                Time = DateTime.UtcNow.AddDays(-5)
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourProblemReportDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.TourId.ShouldBe(updatedEntity.TourId);
            result.Category.ShouldBe(updatedEntity.Category);
            result.Priority.ShouldBe(updatedEntity.Priority); 
            result.Description.ShouldBe(updatedEntity.Description);
            result.Time.ShouldBe(updatedEntity.Time);

            // Assert - Database
            var storedEntity = dbContext.TourProblemReports.FirstOrDefault(i => i.Id == updatedEntity.Id);
            storedEntity.ShouldNotBeNull();
            var storedPriority = (ProblemPriority)storedEntity.Priority;
            storedPriority.CompareTo(updatedEntity.Priority);

            // Ažurira staru vrednost
            var oldEntity = dbContext.TourProblemReports.FirstOrDefault(i => i.Id == -1 && i.Category == "Stari problem");
            oldEntity.ShouldBeNull(); 
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourProblemReportDto
            {
                Id = -1000, 
                TourId = 1, 
                Category = "Tehnički problem",
                Priority = ProblemPriority.MEDIUM,
                Description = "Problem sa internet konekcijom.",
                Time = DateTime.Now
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404); 
        }


        private static TourProblemReportController CreateController(IServiceScope scope)
        {
            return new TourProblemReportController(scope.ServiceProvider.GetRequiredService<ITourProblemReportService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
