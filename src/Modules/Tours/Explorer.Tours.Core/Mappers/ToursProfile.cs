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
        CreateMap<TouristLocationDto, TouristLocation>().ReverseMap();
        CreateMap<CompletedKeyPointsDto, CompletedKeyPoints>().ReverseMap();
        CreateMap<TourExecutionDto, TourExecution>()
                .ForMember(dest => dest.CompletedKeyPoints,
                 opt => opt.MapFrom(src => src.CompletedKeyPoints != null
                ? src.CompletedKeyPoints
                .Select(point => new CompletedKeyPoints(point.ExecutionTime, point.CompletedKeyPointId))
                .ToList()
            : new List<CompletedKeyPoints>())).ReverseMap();

        CreateMap<TourDurationDto, TourDuration>().ReverseMap();
        CreateMap<TourExecutionDto, TourExecution>().ReverseMap();
        CreateMap<BasicTourDetailsDto, Tour>().ReverseMap();
        CreateMap<TourDto,Tour>()
                .ForMember(dur=>dur.TourDurations,
                 opt=>opt.MapFrom(src=>src.TourDurations != null
                ? src.TourDurations
                .Select(duration=>new TourDuration(duration.Duration,(Domain.TransportType)duration.TransportType,(Domain.TimeUnit)duration.TimeUnit))
                .ToList()
                :new List<TourDuration>())).ReverseMap();
    }
}