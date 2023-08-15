using ADecor.Dto;
using ADecor.Endpoints.Brands.Commands;
using ADecor.Endpoints.Brands.Endpoints;
using ADecor.Endpoints.Categories.Commands;
using Ardalis.ApiEndpoints;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace ADecor.Endpoints.Categories
{
    [Authorize(Roles = "Manager")]
    public class CreateCategory : EndpointBaseAsync
        .WithRequest<string>
        .WithActionResult<CategoryModel>
    {
        private readonly IMediator mediator;

        public CreateCategory(IMediator mediator)
        {

            this.mediator = mediator;
        }
        [HttpPost("categories/{Name}")]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation(Summary = "Create Category",
          Description = "Insert only Name of a new Category",
          OperationId = "Category.Create",
          Tags = new[] { "CategoryEndpoint" })]
        public override async Task<ActionResult<CategoryModel>> HandleAsync([FromRoute] string Name, CancellationToken cancellationToken = default)
        {
            var newCat = await mediator.Send(new CreateCategoryCommand { CategoryName = Name});
            return Ok(newCat);
        }
    }
}
