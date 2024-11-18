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
    public class HiddenEncounterService : CrudService<HiddenLocationEncounterDto, HiddenLocationEncounter>, IHiddenEncounterService
    {
        private readonly IMapper _mapper;
        private readonly IHiddenEncounterRepository _hiddenEncounterRepository;
        public HiddenEncounterService(IHiddenEncounterRepository repository, IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
            _hiddenEncounterRepository = repository;
        }

        
        public Result<HiddenLocationEncounterDto> GetById(long id)
        {
            var result = _hiddenEncounterRepository.GetById(id);
            return MapToDto(result);
        }

        public Result<PagedResult<HiddenLocationEncounterDto>> GetPaged(int page, int pageSize)
        {
            var result = _hiddenEncounterRepository.GetPaged(page, pageSize);
            return MapToDto(result);
        }

        public Result<HiddenLocationEncounterDto> Update(HiddenLocationEncounterDto execution)
        {
            try
            {
                var result = _hiddenEncounterRepository.Update(MapToDomain(execution));
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

        public Result<HiddenLocationEncounterDto> Create(HiddenLocationEncounterDto encounter)
        {
            var result = _hiddenEncounterRepository.Create(MapToDomain(encounter));
            return MapToDto(result);
        }
    }
}
