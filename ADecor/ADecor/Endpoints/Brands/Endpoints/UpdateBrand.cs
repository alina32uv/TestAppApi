using ADecor.Data;
using ADecor.Endpoints.Brands.Commands;
using ADecor.Endpoints.Brands.Queries;
using ADecor.Entities;
using Ardalis.ApiEndpoints;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace ADecor.Endpoints.Brands.Endpoints
{
    [Authorize(Roles = "Manager")]
    public class UpdateBrand : EndpointBaseAsync
   .WithRequest<int>
      .WithActionResult<Brand>
    {
        private readonly IMediator _mediator;
        private readonly ADecorContext _ctx;


        public UpdateBrand(IMediator mediator, ADecorContext ctx)
        {
            _mediator = mediator;
            _ctx = ctx;
        }

        [HttpPut("brand/{id:int}/{name}")]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation(Summary = "Update brand by id",
           Description = "Input an int value to update Brand by its Id",
           OperationId = "Brand.Update",
           Tags = new[] { "BrandEndpoint" })]
       

        public override async Task<ActionResult<Brand>> HandleAsync( int id, CancellationToken cancellationToken = default)
        {
            var brand = await _mediator.Send(new GetBrandQuery { BrandId = id });
            if (brand != null)
            {
                var newName = Request.RouteValues["name"] as string;
                await _mediator.Send(new UpdateBrandCommand { BrandId = id , Name = newName});
                _ctx.SaveChanges();
            }


            return brand;
        }

        
    }
}
