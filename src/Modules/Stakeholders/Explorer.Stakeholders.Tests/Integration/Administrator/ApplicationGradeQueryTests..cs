using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Explorer.Stakeholders.Tests.Integration.Administration
{
    public class ApplicationGradeQueryTests : BaseStakeholdersIntegrationTest
    {
        public ApplicationGradeQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = controller.ReviewGrades(1, 10) as OkObjectResult;

            // Provera da li je vraćen rezultat tipa OkObjectResult
            result.ShouldNotBeNull();

            // Kastovanje vrednosti u PagedResult<ApplicationGradeDto>
            var pagedResult = result.Value as Explorer.Stakeholders.API.Public.PagedResult<ApplicationGradeDto>;

            // Provera da li je PagedResult uspešno kastovan
            pagedResult.ShouldNotBeNull();
            pagedResult.Items.Count.ShouldBeGreaterThan(0);
            pagedResult.TotalCount.ShouldBeGreaterThan(0);
        }

        private static ReviewGradeController CreateController(IServiceScope scope)
        {
            return new ReviewGradeController(scope.ServiceProvider.GetRequiredService<IApplicationGradeService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}

