using AutoMapper;
using FluentResults;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using System.Collections.Generic;
using System.Linq;
using Explorer.Stakeholders.API.Public;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ApplicationGradeService : CrudService<ApplicationGradeDto, ApplicationGrade>, IApplicationGradeService
    {
        private readonly IMapper _mapper;

        public ApplicationGradeService(ICrudRepository<ApplicationGrade> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _mapper = mapper;
        }


        // Implementacija GetGrades metode
        public Result<Explorer.Stakeholders.API.Public.PagedResult<ApplicationGradeDto>> GetGrades(int page, int pageSize)
        {
            var gradesPaged = CrudRepository.GetPaged(page, pageSize);  // Paginacija ocena
            var dtoList = _mapper.Map<List<ApplicationGradeDto>>(gradesPaged.Results);

            // Koristimo tačnu verziju PagedResult iz namespace-a Explorer.Stakeholders.API.Public
            var pagedResult = new Explorer.Stakeholders.API.Public.PagedResult<ApplicationGradeDto>
            {
                Items = dtoList,
                TotalCount = gradesPaged.TotalCount,
                Page = page,
                PageSize = pageSize
            };

            return Result.Ok(pagedResult);  // Vraća tačan tip Result<PagedResult<ApplicationGradeDto>>
        }


        // Implementacija AddGrade metode
        public Result<ApplicationGradeDto> AddGrade(ApplicationGradeDto applicationGrade)
        {
            applicationGrade.Created = DateTime.UtcNow;

            var newGrade = _mapper.Map<ApplicationGrade>(applicationGrade);
            CrudRepository.Create(newGrade);

            return Result.Ok(_mapper.Map<ApplicationGradeDto>(newGrade));
        }

        // Implementacija UserExists metode
        public Result<bool> UserExists(int userId)
        {
            var gradesPaged = CrudRepository.GetPaged(0, int.MaxValue);  // Dobija sve podatke
            var exists = gradesPaged.Results.Any(g => g.UserId == userId);
            return Result.Ok(exists);
        }

    }
}


