using ADecor.Endpoints.Categories.Query;
using ADecor.Endpoints.Products.Queries;
using ADecor.Entities;
using Ardalis.ApiEndpoints;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace ADecor.Endpoints.Products
{
   [AllowAnonymous]
    public class GetAllProducts : EndpointBaseAsync
         .WithoutRequest
         .WithActionResult<List<Product>>
    {
        private readonly IMediator mediator;

        public GetAllProducts(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("products")]
        [SwaggerOperation(Summary = "Get all products",
           Description = "Get all products",
           OperationId = "Product.GetAll",
           Tags = new[] { "ProductEndpoint" })]
        public override async Task<ActionResult<List<Product>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var prod = await mediator.Send(new GetAllProductsQuery());

            return Ok(prod);
        }
    }
}
