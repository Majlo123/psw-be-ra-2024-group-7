using Explorer.API.Controllers.Tourist.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration
{
    public class TourProblemReportQueryTests : BaseToursIntegrationTest
    {
        public TourProblemReportQueryTests(ToursTestFactory factory) : base(factory) { }
        [Fact]

        public void Retrieves_all()
        {

            //Arragne
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            //Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourProblemReportDto>;


            //Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
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
