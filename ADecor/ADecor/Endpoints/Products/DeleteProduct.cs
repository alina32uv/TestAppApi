using ADecor.Endpoints.Brands.Commands;
using ADecor.Endpoints.Brands.Queries;
using ADecor.Endpoints.Products.Commands;
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
    [Authorize(Roles = "Manager")]
    public class DeleteProduct : EndpointBaseAsync
   .WithRequest<int>
      .WithActionResult<Product>
    {
        private readonly IMediator _mediator;
        


        public DeleteProduct(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpDelete("products/{id:int}")]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation(Summary = "Delete product by id",
          Description = "Input an int value to delete Product by its Id",
          OperationId = "Product.Delete",
          Tags = new[] { "ProductEndpoint" })]
        public override async  Task<ActionResult<Product>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var prod = await _mediator.Send(new GetProductByIdQuery { ProductId = id });
            if (prod != null)
            {
                await _mediator.Send(new DeleteProductCommand { ProductId = id });
            }


            return prod;
        }
    }
}
