using ADecor.Data;
using ADecor.Entities;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using ADecor.Dto;
using Azure;
using Swashbuckle.AspNetCore.Annotations;
using ADecor.Endpoints.Brands.Commands;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ADecor.Endpoints.Brands.Endpoints
{
    [Authorize(Roles = "Manager")]
    public class AddBrandRequest
    {
        [FromBody] public BrandForCreationDto createdBrand { get; set; } = default!;

    }
    public class AddBrand : EndpointBaseAsync
        .WithRequest<AddBrandRequest>
        .WithActionResult<BrandForCreationDto>
    {
        private readonly IMediator mediator;

        public AddBrand( IMediator mediator)
        {
           
            this.mediator = mediator;
        }
        [HttpPost("brands")]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation(Summary = "Create Brand",
           Description = "Insert only Name of a new Brand",
           OperationId = "Brand.Create",
           Tags = new[] { "BrandEndpoint" })]
        public override async Task<ActionResult<BrandForCreationDto>> HandleAsync(AddBrandRequest request, CancellationToken cancellationToken = default)
        {

            var newBrand = await mediator.Send(new CreateBrandCommand(request.createdBrand.Name));
            return Ok(newBrand);

        }
    }
}
