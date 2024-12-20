using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ToursContext _dbContext;
        private readonly DbSet<Quiz> _dbSet;

        public QuizRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Quiz>();
        }

        public Quiz Create(Quiz quiz)
        {
            _dbSet.Add(quiz);
            _dbContext.SaveChanges();
            return quiz;
        }

        public Quiz Get(long id)
        {
            return _dbSet.Include(q => q.Questions)
                         .ThenInclude(q => q.Answers)
                         .FirstOrDefault(q => q.Id== id);
        }
        public IEnumerable<Quiz> GetAll()
        {
            return _dbContext.Quizzes.ToList();
        }
    }
}
