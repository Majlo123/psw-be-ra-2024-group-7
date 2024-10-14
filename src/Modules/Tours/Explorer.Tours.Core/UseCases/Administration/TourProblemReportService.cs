using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Public.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using AutoMapper;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourProblemReportService : CrudService<TourProblemReportDto, TourProblemReport>, ITourProblemReportService
    {
        public TourProblemReportService(ICrudRepository<TourProblemReport> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
