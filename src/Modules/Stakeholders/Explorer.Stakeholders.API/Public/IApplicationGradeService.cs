using FluentResults;
using Explorer.Stakeholders.API.Dtos;

namespace Explorer.Stakeholders.API.Public
{
    public interface IApplicationGradeService
    {
        Result<ApplicationGradeDto> AddGrade(ApplicationGradeDto applicationGrade);
        Result<PagedResult<ApplicationGradeDto>> GetGrades(int page, int pageSize);  // Ova metoda vraća PagedResult

        Result<bool> UserExists(int userId);
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
