using ADecor.Data;
using ADecor.Endpoints.Categories.Query;
using ADecor.Endpoints.Products.Queries;
using ADecor.Entities;
using Ardalis.ApiEndpoints;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace ADecor.Endpoints.Products
{
    
    public class GetProductById : EndpointBaseAsync
    .WithRequest<int>
      .WithActionResult<Product>
    {
        private readonly IMediator _mediator;



        public GetProductById(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("products/{id:int}")]
        [Authorize]
        [SwaggerOperation(Summary = "Get product by id",
            Description = "Input an int value to find Product by its Id",
            OperationId = "Product.GetById",
            Tags = new[] { "ProductEndpoint" })]
        public override async Task<ActionResult<Product>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var cat = await _mediator.Send(new GetProductByIdQuery { ProductId = id });

            return Ok(cat);
        }
    }

}
