using ADecor.Endpoints.Brands.Commands;
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
    [Authorize(Roles = "Manager")]
    public class DeleteBrand : EndpointBaseAsync
   .WithRequest<int>
      .WithActionResult<Brand>
    {
        private readonly IMediator _mediator;
       


        public DeleteBrand(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("brand/{id:int}")]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation(Summary = "Delete brand by id",
           Description = "Input an int value to delete Brand by its Id",
           OperationId = "Brand.Delete",
           Tags = new[] { "BrandEndpoint" })]
        public override async Task<ActionResult<Brand>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var brand = await _mediator.Send(new GetBrandQuery { BrandId = id });
            if (brand != null)
            {
                await _mediator.Send(new DeleteBrandCommand { BrandId = id });
            }


            return brand;
        }
    }
}
