
using ADecor.Endpoints.Categories.Query;
using ADecor.Entities;
using Ardalis.ApiEndpoints;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ADecor.Endpoints.Categories
{
    [Authorize]
    public class GetAllCategories : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<Category>>
    {
        private readonly IMediator mediator;

        public GetAllCategories(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("categories")]
        [Authorize]
        [SwaggerOperation(Summary = "Get all categories",
           Description = "Get all categories",
           OperationId = "Category.GetAll",
           Tags = new[] { "CategoryEndpoint" })]
        public override async Task<ActionResult<List<Category>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var cat = await mediator.Send(new GetAllCategoriesQuery());

            return Ok(cat);
        }
    }
}
