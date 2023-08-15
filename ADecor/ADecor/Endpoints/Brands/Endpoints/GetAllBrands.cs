using ADecor.Data;
using ADecor.Endpoints.Brands.Queries;
using ADecor.Entities;
using Ardalis.ApiEndpoints;
using Azure;
using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace ADecor.Endpoints.Brands.Endpoints
{
    [Authorize]
    public class GetAllBrands : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<Brand>>
    {

        private readonly IMediator mediator;
        private readonly IMemoryCache _memoryCache;

        public GetAllBrands( IMediator mediator, IMemoryCache memoryCache)
        {
            this.mediator = mediator;
            _memoryCache = memoryCache;
        }

        [HttpGet("brands")]
        [Authorize]
        [SwaggerOperation(Summary = "Get all brands",
           Description = "Get all brands",
           OperationId = "Brand.GetAll",
           Tags = new[] { "BrandEndpoint" })]

        public override async Task<ActionResult<List<Brand>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            if (_memoryCache.TryGetValue("Brand.GetAll", out List<Brand> cachedBrands))
            {
                return Ok(cachedBrands);
            }

            var brand = await mediator.Send(new GetAllBrandsQuery());
            _memoryCache.Set("Brand.GetAll", brand, TimeSpan.FromHours(2));
            return Ok(brand);

        }
        
    }
}
