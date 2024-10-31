using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.TourProblemReports;
using Explorer.Stakeholders.Core.Mappers;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Stakeholders.Infrastructure;

public static class StakeholdersStartup
{
    public static IServiceCollection ConfigureStakeholdersModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StakeholderProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IPersonEditingService, PersonEditingService>();
        services.AddScoped<IPersonRepository, PersonDatabaseRepository>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenGenerator, JwtGenerator>();
        services.AddScoped<ITourProblemReportService, TourProblemReportService>();


        services.AddScoped<IToursitClubService, TouristClubService>();

        services.AddScoped<ITouristEquipmentService, TouristEquipmentService>();
        services.AddScoped<ITouristLocationService, TouristLocationService>();
        // Dodajemo servis za ApplicationGrade
        services.AddScoped<IApplicationGradeService, ApplicationGradeService>();

    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Person>), typeof(CrudDatabaseRepository<Person, StakeholdersContext>));
        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<TouristClub>), typeof(CrudDatabaseRepository<TouristClub, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<TouristEquipment>),typeof(CrudDatabaseRepository<TouristEquipment, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<TouristLocation>), typeof(CrudDatabaseRepository<TouristLocation, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<TourProblemReport>), typeof(CrudDatabaseRepository<TourProblemReport, StakeholdersContext>));
        services.AddScoped<ITourProblemReportRepository, TourProblemReportRepository>();
        services.AddScoped<IPersonRepository, PersonDatabaseRepository>();
        // Dodajemo repozitorijum za ApplicationGrade
        services.AddScoped(typeof(ICrudRepository<ApplicationGrade>), typeof(CrudDatabaseRepository<ApplicationGrade, StakeholdersContext>));

        services.AddDbContext<StakeholdersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("stakeholders"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stakeholders")));
    }
}