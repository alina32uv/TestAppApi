using ADecor.Endpoints.Categories.Commands;
using ADecor.Endpoints.Categories;
using Ardalis.ApiEndpoints;
using ADecor.Endpoints.Products.Commands;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ADecor.Endpoints.Products
{
    [Authorize(Roles = "Manager")]
    public class CreateProduct : EndpointBaseAsync
        .WithRequest<CreateProductCommand>
        .WithActionResult<ProductModel>
    {
        private readonly IMediator mediator;

        public CreateProduct(IMediator mediator)
        {

            this.mediator = mediator;
        }
        [HttpPost("products")]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation(Summary = "Create Product",
          Description = "Insert attributes for a new Product",
          OperationId = "Product.Create",
          Tags = new[] { "ProductEndpoint" })]
        public override async Task<ActionResult<ProductModel>> HandleAsync(CreateProductCommand request, CancellationToken cancellationToken = default)
        {
            var newProd = await mediator.Send(new CreateProductCommand(request.prodModel));
            return Ok(newProd);
        }
    }
}
