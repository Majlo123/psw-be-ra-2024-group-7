using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Administration
{

    [Collection("Sequential")]
    public class TourExecutionCommandTests : BaseToursIntegrationTest
    {
        public TourExecutionCommandTests(ToursTestFactory factory) : base(factory) { }
        [Fact]
        public void CheckLocation_ReturnsExpectedResult()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            var touristLocation = new TouristLocationDto
            {
                TouristId = -22,
                Latitude = 10.0F,
                Longitude = 10.1F
            };

            // Act
            var actionResult = controller.CheckLocation(touristLocation, -6);
            var result = (actionResult as ObjectResult)?.Value as TourExecutionDto;

            // Assert-response
            result.ShouldNotBeNull();
            result.CurrentLatitude.ShouldBe(touristLocation.Latitude);
            result.CurrentLongitude.ShouldBe(touristLocation.Longitude);
            result.CompletedKeyPoints.Count.ShouldBe(2);

            //Assert-database
            var storedTourExecution = dbContext.TourExecutions.FirstOrDefault(t => t.Id == -6);
            storedTourExecution.ShouldNotBeNull();
            storedTourExecution.CurrentLatitude.ShouldBe(touristLocation.Latitude);
            storedTourExecution.CurrentLongitude.ShouldBe(touristLocation.Longitude);
            storedTourExecution.CompletedKeyPoints.Count.ShouldBe(2);
        }
        private static TourExecutionController CreateController(IServiceScope scope)
        {
            return new TourExecutionController(scope.ServiceProvider.GetRequiredService<ITourExecutionService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
