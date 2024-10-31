using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
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

        CreateMap<CompletedKeyPointsDto, CompletedKeyPoints>().ReverseMap();
        CreateMap<TourExecutionDto, TourExecution>()
                .ForMember(dest => dest.CompletedKeyPoints,
                 opt => opt.MapFrom(src => src.CompletedKeyPoints != null
                ? src.CompletedKeyPoints
                .Select(point => new CompletedKeyPoints(point.ExecutionTime, point.CompletedKeyPointId))
                .ToList()
            : new List<CompletedKeyPoints>())).ReverseMap();

    }
}