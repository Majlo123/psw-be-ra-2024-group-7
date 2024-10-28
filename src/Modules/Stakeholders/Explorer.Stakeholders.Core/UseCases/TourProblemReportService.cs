using Explorer.BuildingBlocks.Core.UseCases;
using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.TourProblemReports;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TourProblemReportService : CrudService<TourProblemReportDto, TourProblemReport>, ITourProblemReportService
    {
        //private readonly ITourProblemReportRepository _tourProblemReportRepository;
        private readonly ICrudRepository<TourProblemReport> _repository;
        private readonly IMapper _mapper;

        public TourProblemReportService(ICrudRepository<TourProblemReport> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper; 
        }

        public Result<TourProblemReportDto> SetSolvingDeadline(int id, TourProblemReportDto tourProblemReportDto)
        {
            var aggregate = _repository.Get(id);
            if (aggregate == null)
            {
                throw new Exception("Agregat TourProblemReport nije pronađen");
            }

            aggregate.SetSolvingDeadline(tourProblemReportDto.SolvingDeadline);

            _repository.Update(aggregate);

            var dto = _mapper.Map<TourProblemReportDto>(aggregate);
            return Result.Ok();
        }
    }
}
