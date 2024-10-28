using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogService :BaseService<BlogDto, Core.Domain.Blog>, IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        public BlogService(IMapper mapper, IBlogRepository blogRepository) : base(mapper) {
            _blogRepository = blogRepository;
        }

        public Result<BlogDto> Create(BlogDto blog)
        {
            try
            {
                var result = _blogRepository.Create(MapToDomain(blog));
                return MapToDto(result);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<BlogDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Result<PagedResult<BlogDto>> GetPaged(int page, int pageSize)
        {
            var result = _blogRepository.GetPaged(page, pageSize);
            return MapToDto(result);
        }

        public Result<BlogDto> Update(BlogDto blog)
        {
            throw new NotImplementedException();
        }
    }
}
