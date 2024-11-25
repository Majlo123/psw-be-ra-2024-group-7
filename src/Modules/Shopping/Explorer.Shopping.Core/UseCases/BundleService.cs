using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Shopping.API.Dtos;
using Explorer.Shopping.API.Public;
using Explorer.Shopping.Core.Domain;
using Explorer.Shopping.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Shopping.Core.UseCases;

public class BundleService : BaseService<BundleDto, Bundle>, IBundleService
{
    private readonly IBundleRepository _bundleRepository;
    private readonly IProductRepository _productRepository;
    public BundleService(IMapper mapper, IBundleRepository bundleRepository, IProductRepository productRepository) : base(mapper)
    {
        _bundleRepository = bundleRepository;
        _productRepository = productRepository;
    }

    public Result<BundleDto> Create(BundleDto bundle)
    {
        bundle.Status = API.Dtos.BundleStatus.Draft;
        var result = _bundleRepository.Create(MapToDomain(bundle));
        //foreach(var product in bundle.Products)
        //{
        //    _productRepository.Add(new Product);
        //}
        return MapToDto(result);
    }

    public Result Delete(long id)
    {
        try
        {
            var bundle = _bundleRepository.Get(id);
            if(bundle == null)
                throw new Exception("Bundle with this id does not exist.");
            var productIds = bundle.Products.Select(p => p.Id).ToList();
            foreach (var productId in productIds)
            {
                _productRepository.Delete(productId);
            }
            _bundleRepository.Delete(id);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(FailureCode.NotFound).WithError(ex.Message);
        }
    }

    public Result<BundleDto> Get(long id)
    {
        var res = _bundleRepository.Get(id);
        return MapToDto(res);
    }

    public Result<PagedResult<BundleDto>> GetPaged(int page, int pageSize)
    {
        var result = _bundleRepository.GetPaged(page, pageSize);
        return MapToDto(result);
    }

    public Result<BundleDto> Update(BundleDto bundleDto)
    {
        try
        {
            var result = _bundleRepository.Update(MapToDomain(bundleDto));
            return MapToDto(result);
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
       
    }

    public Result<PagedResult<BundleDto>> GetPagedByCreatorId(long creatorId, int page, int pageSize)
    {
        var result = _bundleRepository.GetPagedByCreatorId(creatorId, page, pageSize);
        return MapToDto(result);
    }
}
