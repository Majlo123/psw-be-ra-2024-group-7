using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        // Mapiramo DTO na domensku klasu i obratno
        CreateMap<ApplicationGradeDto, ApplicationGrade>().ReverseMap();
    }
}