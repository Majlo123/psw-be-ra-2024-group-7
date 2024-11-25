using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TourObject = Explorer.Tours.Core.Domain.TourObject;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourObjectService : CrudService<TourObjectDto, TourObject>, ITourObjectService
    {

        private readonly INotificationInternalService _notificationInternalService;

        public TourObjectService(ICrudRepository<TourObject> repository,INotificationInternalService notificationInternalService, IMapper mapper) : base(repository, mapper) {

            _notificationInternalService = notificationInternalService;
        }

        public Result<TourObjectDto> UpdateStatus(TourObjectDto tourObject)
        {
            int authorId = -1;

            authorId = tourObject.AuthorId;

            if (authorId != -1)

            {
                NotificationDto notification = new NotificationDto();
                notification.ReportId = 0;
                notification.RecipientId = authorId;
                notification.IsRead = false;

                if (tourObject.Status == TourObjectDto.ObjectStatus.PUBLIC)
                    notification.NotificationType = Stakeholders.API.Dtos.NotificationType.ACCEPT_OBJ;
                else
                    notification.NotificationType = Stakeholders.API.Dtos.NotificationType.REFUSE_OBJ;
                _notificationInternalService.Create(notification);

                return Update(tourObject);
            }
            else return null;
        }
    }
}
