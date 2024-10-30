using Explorer.API.Controllers.User.TourProblem;
using Explorer.API.Controllers.Tourist.TourProblem;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
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
            var touristController = CreateTouristController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new TourProblemReportDto
            {
                TourId = -1, 
                Category = "Tehnički problem", 
                Priority = ProblemPriority.HIGH, 
                Description = "Problem sa internet konekcijom.", 
                Time = DateTime.UtcNow.AddDays(-2),
                Status = 0,
                TouristId = -21,
                Comment = "Jako losa internet konekcija",
                Messages = new List<MessageDto>(),
                Notifications = new List<NotificationDto>()
            };

            //Act
            var result = ((ObjectResult)touristController.Create(newEntity).Result)?.Value as TourProblemReportDto;

            //Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.TourId.ShouldBe(newEntity.TourId);
            result.Category.ShouldBe(newEntity.Category);
            result.Priority.ShouldBe(newEntity.Priority);
            result.Description.ShouldBe(newEntity.Description);
            result.Time.ShouldBe(newEntity.Time);
            result.Status.ShouldBe(newEntity.Status);
            result.TouristId.ShouldBe(newEntity.TouristId);
            result.Comment.ShouldBe(newEntity.Comment);

            // Assert - Database
            var storedEntity = dbContext.TourProblemReports.FirstOrDefault(i => i.TourId == newEntity.TourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var touristController = CreateTouristController(scope);
            var updatedEntity = new TourProblemReportDto
            {
                Priority = ProblemPriority.HIGH,
                Description = "Problem sa internet konekcijom.",
                Time = DateTime.Now
            };

            // Act
            var result = (ObjectResult)touristController.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var touristController = CreateTouristController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = (OkResult)touristController.Delete(-1);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.TourProblemReports.FirstOrDefault(i => i.Id == -1);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var touristController = CreateTouristController(scope);

            // Act
            var result = (ObjectResult)touristController.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var touristController = CreateTouristController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            var updatedEntity = new TourProblemReportDto
            {
                Id = -2, 
                TourId = -2, 
                Category = "Tehnički problem",
                Priority = ProblemPriority.MEDIUM, 
                Description = "Jako losa internet konekcija.",
                Time = DateTime.UtcNow.AddDays(-5),
                Status = 0,
                TouristId = -21,
                Comment = "aa",
                Messages = new List<MessageDto>(),
                Notifications = new List<NotificationDto>()
            };

            // Act
            var result = ((ObjectResult)touristController.Update(updatedEntity).Result)?.Value as TourProblemReportDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-2);
            result.TourId.ShouldBe(updatedEntity.TourId);
            result.Category.ShouldBe(updatedEntity.Category);
            result.Priority.ShouldBe(updatedEntity.Priority); 
            result.Description.ShouldBe(updatedEntity.Description);
            result.Time.ShouldBe(updatedEntity.Time);
            result.Status.ShouldBe(updatedEntity.Status);
            result.TouristId.ShouldBe(updatedEntity.TouristId);
            result.Comment.ShouldBe(updatedEntity.Comment);

            // Assert - Database
            var storedEntity = dbContext.TourProblemReports.FirstOrDefault(i => i.Description == "Jako losa internet konekcija.");
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);

            // Ažurira staru vrednost
            var oldEntity = dbContext.TourProblemReports.FirstOrDefault(i => i.Id == -2 && i.Category == "Problem sa internet konekcijom.");
            oldEntity.ShouldBeNull(); 
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var touristController = CreateTouristController(scope);
            var updatedEntity = new TourProblemReportDto
            {
                Id = -1000, 
                TourId = 1, 
                Category = "Tehnički problem",
                Priority = ProblemPriority.MEDIUM,
                Description = "Problem sa internet konekcijom.",
                Time = DateTime.Now,
                Status = 0,
                TouristId = -21,
                Comment = "aa"
            };

            // Act
            var result = (ObjectResult)touristController.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404); 
        }
        [Theory]
        [MemberData(nameof(MessageData))]
        public void AddMessage(int userId, TourProblemReportDto report, int expectedResponseCode, MessageDto messageDto)
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var userController = CreateUserController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            // Act
            var result = (ObjectResult)userController.AddMessage(messageDto, userId, report.Id).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);

            // Assert - Database
            var storedEntity = dbContext.TourProblemReports.FirstOrDefault(r => r.Id == report.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Messages.ShouldContain(m =>
                m.UserId == messageDto.UserId &&
                m.ReportId == messageDto.ReportId &&
                m.Content == messageDto.Content);
        }

        public static IEnumerable<object[]> MessageData()
        {
            return new List<object[]>
            {
                new object[]
                {
                    -3,
                    new TourProblemReportDto
                    {
                        Id = -3,
                        TourId = -2,
                        Category = "Oprema",
                        Priority = ProblemPriority.LOW,
                        Description = "Oprema nije u dobrom stanju",
                        Time = DateTime.UtcNow,
                        Status = Status.UNSOLVED,
                        TouristId = -3,
                        Comment = "aaa"
                    },
                    200,
                    new MessageDto
                    {
                        UserId = -3,
                        ReportId = -3,
                        Content = "Da li je moguće dobiti povrat novca?"
                    }
                }
            };
        }

        private static TourProblemUserController CreateUserController(IServiceScope scope)
        {
            return new TourProblemUserController(scope.ServiceProvider.GetRequiredService<ITourProblemReportService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
        private static TourProblemTouristController CreateTouristController(IServiceScope scope)
        {
            return new TourProblemTouristController(scope.ServiceProvider.GetRequiredService<ITourProblemReportService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
