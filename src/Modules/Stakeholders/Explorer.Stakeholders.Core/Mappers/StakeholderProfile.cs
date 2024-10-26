using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {

        CreateMap<TouristClubDto, TouristClub>().ReverseMap();



        CreateMap<TouristEquipmentDto, TouristEquipment>().ReverseMap();
        CreateMap<PersonDto, Person>().ReverseMap();
        CreateMap<TourProblemReportDto, TourProblemReport>().ReverseMap();
        // Mapiramo DTO na domensku klasu i obratno
        CreateMap<ApplicationGradeDto, ApplicationGrade>().ReverseMap();
        CreateMap<TouristLocationDto, TouristLocation>().ReverseMap();
    }
}