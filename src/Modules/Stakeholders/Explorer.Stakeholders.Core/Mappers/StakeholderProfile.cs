using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.TourProblemReports;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {

        CreateMap<TouristClubDto, TouristClub>().ReverseMap();



        CreateMap<TouristEquipmentDto, TouristEquipment>().ReverseMap();
        CreateMap<PersonDto, Person>().ReverseMap();
        CreateMap<TourProblemReportDto, TourProblemReport>().ReverseMap();
        CreateMap<MessageDto, Message>().ReverseMap();
        CreateMap<NotificationDto, Notification>().ReverseMap();
        // Mapiramo DTO na domensku klasu i obratno
        CreateMap<ApplicationGradeDto, ApplicationGrade>().ReverseMap();
    }
}