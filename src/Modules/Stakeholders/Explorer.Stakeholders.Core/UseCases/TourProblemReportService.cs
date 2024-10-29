using Explorer.BuildingBlocks.Core.UseCases;
using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.TourProblemReports;
using Explorer.Tours.API.Internal;
using FluentResults;
using ProblemPriority = Explorer.Stakeholders.API.Dtos.ProblemPriority;
using Status = Explorer.Stakeholders.API.Dtos.Status;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TourProblemReportService : CrudService<TourProblemReportDto, TourProblemReport>, ITourProblemReportService
    {
        private readonly ITourProblemReportRepository _tourProblemReportRepository;
        private readonly IInternalTourService _internalTourService;
        public TourProblemReportService(ICrudRepository<TourProblemReport> repository, IMapper mapper, ITourProblemReportRepository tourProblemReportRepository, IInternalTourService internalTourService) : base(repository, mapper)
        {
            _tourProblemReportRepository = tourProblemReportRepository;
            _internalTourService = internalTourService;
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
                    Comment = report.Comment
                };
                authorReports.Add(reportDto);
            }

            return new PagedResult<TourProblemReportDto>(authorReports, authorReports.Count);
        }


        //Iva dodala za notifikacije
        public List<NotificationDto> GetLoggedUserNotifications(int loggedId, int page, int pageSize)
        {
            // Retrieve the list of tour problem reports
            var tourProblems = _tourProblemReportRepository.GetPaged(page, pageSize).Results;

            // Get notifications for the logged-in user
            var notifications = new TourProblemReport().getLoggedUserNotifications(tourProblems, loggedId);

            // Map Notification to NotificationDto
            return notifications.Select(notification => new NotificationDto
            {
                SenderId = notification.SenderId,
                RecipientId = notification.RecipientId,
                IsRead = notification.IsRead,
                NotificationType = (API.Dtos.NotificationType)notification.NotificationType,
                Content = notification.Content
            }).ToList();
        }
        //

    }
}
