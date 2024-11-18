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
    public class MiscEncounterService : CrudService<MiscEncounterDto, MiscEncounter>, IMiscEncounterService
    {
        private readonly IMapper _mapper;
        private readonly IMiscEncounterRepository _miscEnconterRepository;

        public MiscEncounterService(IMapper mapper, IMiscEncounterRepository miscEnconterRepository) : base(mapper)
        {
            _mapper = mapper;
            _miscEnconterRepository = miscEnconterRepository;
        }

        public Result<MiscEncounterDto> GetById(long id)
        {
            var result = _miscEnconterRepository.GetById(id);
            return MapToDto(result);
        }

        public Result<PagedResult<MiscEncounterDto>> GetPaged(int page, int pageSize)
        {
            var result = _miscEnconterRepository.GetPaged(page, pageSize);
            return MapToDto(result);
        }

        public Result<MiscEncounterDto> Update(MiscEncounterDto execution)
        {
            try
            {
                var result = _miscEnconterRepository.Update(MapToDomain(execution));
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

        public Result<MiscEncounterDto> Create(MiscEncounterDto encounter)
        {
            var result = _miscEnconterRepository.Create(MapToDomain(encounter));
            return MapToDto(result);
        }
    }
}
