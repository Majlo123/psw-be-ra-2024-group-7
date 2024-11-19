using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Shopping.API.Dtos;
using Explorer.Shopping.API.Public;
using Explorer.Shopping.Core.Domain.RepositoryInterfaces;
using Explorer.Shopping.Core.Domain;
using FluentResults;
using Explorer.Stakeholders.API.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Shopping.Core.UseCases
{
    public class TouristWalletService : BaseService<TouristWalletDto, TouristWallet>, ITouristWalletService
    {
        private readonly ITouristWalletRepository _repository;
        private readonly INotificationService _internalNotificationService;

        public TouristWalletService(ITouristWalletRepository repository, IMapper mapper, INotificationService internalNotificationService) : base(mapper)
        {
            _repository = repository;
            _internalNotificationService = internalNotificationService;
        }

        public Result<TouristWalletDto> Create(TouristWalletDto dto)
        {
            var wallet = MapToDomain(dto);
            _repository.Create(wallet);
            return dto;
        }

        public Result<TouristWalletDto> GetAdventureCoins(long userId)
        {
            var wallet = _repository.GetByUser(userId);
            return MapToDto(wallet);
        }

        public Result<TouristWalletDto> PaymentAdventureCoins(int userId, int coins)
        {
            var wallet = _repository.PaymentAdventureCoins(userId, coins);
            NotificationDto notification = new NotificationDto();
            notification.ReportId = 0;
            notification.RecipientId = userId;
            notification.IsRead = false;
            notification.NotificationType = NotificationType.PAYMENT;
            _internalNotificationService.Create(notification);
            return MapToDto(wallet);
        }
    }
}
