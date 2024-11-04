using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using AutoMapper;

namespace Explorer.Blog.API.Public
{
    public interface IBlogService
    {
        Result<PagedResult<BlogDto>> GetPaged(int page, int pageSize);
        Result<BlogDto> Get(int id);
        Result<BlogDto> Create(BlogDto blog);
        Result<BlogDto> Update(BlogDto blog);
        Result<BlogDto> UpdateRating(int id, RatingDto rationg);
    }
}
