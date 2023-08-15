using ADecor.Endpoints.Brands.Queries;
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
    public class GetCategoryById : EndpointBaseAsync
    .WithRequest<int>
      .WithActionResult<Category>
    {
        private readonly IMediator _mediator;



        public GetCategoryById(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("categories/{id:int}")]
        [Authorize]
        [SwaggerOperation(Summary = "Get category by id",
            Description = "Input an int value to find Category by its Id",
            OperationId = "Category.GetById",
            Tags = new[] { "CategoryEndpoint" })]
        public override async Task<ActionResult<Category>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var cat = await _mediator.Send(new GetCategoryByIdQuery { CategoryId = id });

            return Ok(cat);
        }
    }
}
