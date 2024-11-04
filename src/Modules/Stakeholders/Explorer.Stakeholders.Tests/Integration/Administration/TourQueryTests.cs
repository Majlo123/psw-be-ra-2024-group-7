using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Administration;

[Collection("Sequential")]
public class TourQueryTests : BaseStakeholdersIntegrationTest
{
    public TourQueryTests(StakeholdersTestFactory factory) : base(factory)
    {
    }
    [Fact]
    public void Retrives_all_published()
    {
        //Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        //Act
        var result = ((ObjectResult)controller.GetPublished(0, 0).Result).Value as PagedResult<BasicTourDetailsDto>;

        //Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(1);
        result.TotalCount.ShouldBe(1);
    }

    [Fact]
    public async void Retreives_published_by_id()
    {
        //Arragne
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        //Act
        var result = ((ObjectResult)controller.GetPublishedTourById(-5).Result)?.Value as TourDto;

        //Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-5);
        result.Name.ShouldBe("Tura5");
    }
    private static TourController CreateController(IServiceScope scope)
    {
        return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
