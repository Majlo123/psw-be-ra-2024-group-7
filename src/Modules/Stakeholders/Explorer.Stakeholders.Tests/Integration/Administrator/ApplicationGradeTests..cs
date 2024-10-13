using Explorer.Stakeholders.API.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.API.Public;
using Explorer.API.Controllers.Administrator.Administration;

namespace Explorer.Stakeholders.Tests.Integration.Administration
{
    public class ApplicationGradeTests : BaseStakeholdersIntegrationTest
    {
        public ApplicationGradeTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Give_higher_rating()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dto = new ApplicationGradeDto
            {
                Rating = 6,
                Comment = "This is the best app I have ever used"
            };

            // Act
            var ex = Assert.Throws<ArgumentException>(() => controller.AddGrade(dto));

            // Assert
            Assert.Equal("Invalid rating", ex.Message);
        }

        [Fact]
        public void Add_empty_comment()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dto = new ApplicationGradeDto
            {
                UserId = -1,
                Created = DateTime.Now,
                Rating = 4,
                Comment = "No comment added."
            };

            // Act
            var result = (ObjectResult)controller.AddGrade(dto);

            // Assert
            result.StatusCode.ShouldBe(200);
        }

        private static ReviewGradeController CreateController(IServiceScope scope)
        {
            return new ReviewGradeController(scope.ServiceProvider.GetRequiredService<IApplicationGradeService>());
        }
    }
}

