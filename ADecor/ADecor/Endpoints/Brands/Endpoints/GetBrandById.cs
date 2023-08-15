using ADecor.Data;
using ADecor.Endpoints.Brands.Queries;
using ADecor.Entities;
using Ardalis.ApiEndpoints;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ADecor.Endpoints.Brands.Endpoints
{
    [Authorize]
    public class GetBrandById : EndpointBaseAsync
    .WithRequest<int>
      .WithActionResult<Brand>
    {
        private readonly IMediator _mediator;
        


        public GetBrandById(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("branda/{id:int}")]
        [Authorize]
        [SwaggerOperation(Summary = "Get brand by id",
            Description = "Input an int value to find Brand by its Id",
            OperationId = "Brand.GetById",
            Tags = new[] { "BrandEndpoint" })]
        public override async Task<ActionResult<Brand>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var brand = await _mediator.Send(new GetBrandQuery { BrandId = id});            

            return Ok(brand);
        }


    }
}
