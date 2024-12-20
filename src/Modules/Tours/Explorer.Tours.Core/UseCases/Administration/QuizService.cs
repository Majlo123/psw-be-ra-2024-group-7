using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class QuizService : CrudService<QuizDto, Quiz>, IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;

        public QuizService(ICrudRepository<Quiz> repository,IQuizRepository quizRepository, IMapper mapper)
            : base(repository, mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }
        public PagedResult<QuizDto> GetAllQuizzes(int page, int pageSize)
        {
            try
            {
                // Dobavljanje kvizova iz baze
                var quizzes = _quizRepository.GetAll()
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Mapiranje domen -> DTO
                var quizDtos = _mapper.Map<List<QuizDto>>(quizzes);

                // Ukupan broj kvizova
                var totalCount = _quizRepository.GetAll().Count();

                return new PagedResult<QuizDto>(quizDtos, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve quizzes.", ex);
            }
        }
        public Result<QuizResponseDto> CreateQuiz(QuizDto dto)
        {
            try
            {
                // Mapiranje DTO -> Domen
                var quiz = _mapper.Map<Quiz>(dto);

                // Čuvanje kviza u bazi
                var createdQuiz = _quizRepository.Create(quiz);

                // Generisanje nagrade
                var reward = new Reward(RewardType.XP, 100);

                // Mapiranje domen -> Response DTO
                var response = new QuizResponseDto
                {
                    QuizId = ((int)createdQuiz.Id),
                    Message = "Quiz created successfully.",
                    Reward = _mapper.Map<RewardDto>(reward)
                };

                return Result.Ok(response);
            }
            catch (Exception ex)
            {
                return Result.Fail("Failed to create quiz").WithError(ex.Message);
            }
        }
    }
}
