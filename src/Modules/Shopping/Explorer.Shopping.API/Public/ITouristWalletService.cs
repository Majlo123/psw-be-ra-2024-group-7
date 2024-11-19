using Explorer.Shopping.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Shopping.API.Public
{
    public interface ITouristWalletService
    {
        public Result<TouristWalletDto> GetAdventureCoins(long userId);
        public Result<TouristWalletDto> PaymentAdventureCoins(int userId, int coins);
        public Result<TouristWalletDto> Create(TouristWalletDto dto);
    }
}
