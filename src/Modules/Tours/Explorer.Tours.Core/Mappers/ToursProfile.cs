using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using TourObject = Explorer.Tours.Core.Domain.TourObject;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        CreateMap<TourEquipmentDto, TourEquipment>().ReverseMap();
        CreateMap<TourReviewDto, TourReview>().ReverseMap();
        CreateMap<KeyPointDto, KeyPoint>().ReverseMap();
        CreateMap<TourDto, Tour>().ReverseMap();
        CreateMap<TourObjectDto, TourObject>().ReverseMap();
        CreateMap<TourDurationDto, TourDuration>().ReverseMap();
    }
}