using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class SocialEncounterService : CrudService<SocialEncounterDto, SocialEncounter>, ISocialEncounterService
    {
        private readonly IMapper _mapper;
        private readonly ISocialEncounterRepository _socialEncounterRepository;
        public SocialEncounterService(ISocialEncounterRepository repository, IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
            _socialEncounterRepository = repository;
        }
        public Result<SocialEncounterDto> GetById(long id)
        {
            var result = _socialEncounterRepository.GetById(id);
            return MapToDto(result);
        }

        public Result<PagedResult<SocialEncounterDto>> GetPaged(int page, int pageSize)
        {
            var result = _socialEncounterRepository.GetPaged(page, pageSize);
            return MapToDto(result);
        }

        public Result<SocialEncounterDto> Update(SocialEncounterDto execution)
        {
            try
            {
                var result = _socialEncounterRepository.Update(MapToDomain(execution));
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

        public Result<SocialEncounterDto> Create(SocialEncounterDto encounter)
        {
            var result = _socialEncounterRepository.Create(MapToDomain(encounter));
            return MapToDto(result);
        }
    }
}
