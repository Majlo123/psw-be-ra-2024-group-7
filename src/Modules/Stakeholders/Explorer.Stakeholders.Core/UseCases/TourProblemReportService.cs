using Explorer.BuildingBlocks.Core.UseCases;
using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.TourProblemReports;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Explorer.Tours.API.Internal;
using FluentResults;
using ProblemPriority = Explorer.Stakeholders.API.Dtos.ProblemPriority;
using Status = Explorer.Stakeholders.API.Dtos.Status;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TourProblemReportService : CrudService<TourProblemReportDto, TourProblemReport>, ITourProblemReportService
    {
        private readonly IMapper _mapper;
        private readonly ICrudRepository<TourProblemReport> _repository;
        private readonly ITourProblemReportRepository _tourProblemReportRepository;
        private readonly IInternalTourService _internalTourService;
        public TourProblemReportService(ICrudRepository<TourProblemReport> repository, IMapper mapper, ITourProblemReportRepository tourProblemReportRepository, IInternalTourService internalTourService) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _tourProblemReportRepository = tourProblemReportRepository;
            _internalTourService = internalTourService;
        }

        public Result<PagedResult<TourProblemReportDto>> GetPaged(int page, int pageSize)
        {
            var result = _tourProblemReportRepository.GetPaged(page, pageSize);
            return MapToDto(result);
        }

        public Result<PagedResult<TourProblemReportDto>> GetByTouristId(int id, int page, int pageSize)
        {
            var result = _tourProblemReportRepository.GetByTouristId(id, page, pageSize);
            return MapToDto(result);
        }

        public PagedResult<TourProblemReportDto> GetByAuthorId(int authorId, int page, int pageSize)
        {
            var reports = _tourProblemReportRepository.GetPaged(page, pageSize);
            var authorReports = new List<TourProblemReportDto>();

            foreach (var report in reports.Results)
            {
                var tourResult = _internalTourService.Get(report.TourId);
                if(!tourResult.IsSuccess || tourResult.Value.AuthorId != authorId) continue;
                var reportDto = new TourProblemReportDto
                {
                    Id = (int)report.Id,
                    TourId = report.TourId,
                    Category = report.Category,
                    Priority = (ProblemPriority)report.Priority,
                    Description = report.Description,
                    Time = report.Time,
                    Status = (Status)report.Status,
                    TouristId = report.TouristId,
                    Comment = report.Comment,
                    Messages = report.Messages.Select(message => new MessageDto
                    {
                        UserId = message.UserId,
                        ReportId = message.ReportId,
                        Content = message.Content
                    }).ToList()
                };
                authorReports.Add(reportDto);
            }

            return new PagedResult<TourProblemReportDto>(authorReports, authorReports.Count);
        }

        public Result<TourProblemReportDto> AddMessage(MessageDto messageDto, int userId, int reportId)
        {
            try
            {
                var report = _tourProblemReportRepository.Get(reportId);
                if (report == null) throw new Exception("Report not found.");

                var message = _mapper.Map<MessageDto, Message>(messageDto);
                message.UserId = userId;
                message.ReportId = reportId;

                report.AddMessage(message);

                var result = _tourProblemReportRepository.Update(report);

                return MapToDto(result);
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
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
            return Result.Ok(dto);
        }
    }
}
