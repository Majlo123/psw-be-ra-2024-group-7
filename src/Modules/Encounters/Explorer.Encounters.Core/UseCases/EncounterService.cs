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
    public class EncounterService : CrudService<EncounterDto, Encounter>, IEncounterService
    {
        private readonly IMapper _mapper;
        private readonly IEncounterRepository _encounterRepository;
        public EncounterService(IEncounterRepository repository, IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
            _encounterRepository = repository;
        }

        public Result<EncounterDto> CreateEncounter(EncounterDto encounter)
        {
            var result = _encounterRepository.Create(encounter);
            return (result);
        }
        
        public Result<List<EncounterDto>> GetAll()
        {
            var result = _encounterRepository.GetAll();
            return MapToDto(result);
        }

        public Result<List<EncounterDto>> GetTouristRequestEncounters()
        {
            var result = _encounterRepository.GetTouristRequestEncounters();
            return MapToDto(result);
        }

        public Result<EncounterDto> GetById(long id)
        {
            var result = _encounterRepository.GetById(id);
            return MapToDto(result);
        }

        public Result<List<EncounterDto>> GetByTourId(int tourId)
        {
            var result = _encounterRepository.GetAllByTourId(tourId);
            return MapToDto(result);
        }

        public Result<EncounterDto> Update(EncounterDto encounter)
        {
            try
            {
                var result = _encounterRepository.Update((encounter));
                return (result);
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
    }
}
