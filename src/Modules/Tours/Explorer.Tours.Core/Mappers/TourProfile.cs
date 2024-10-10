using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers
{
    public class TourProfile : Profile
    {
        public TourProfile() {

            CreateMap<TourDto, Tour>().ReverseMap();
        }
        
    }
}
