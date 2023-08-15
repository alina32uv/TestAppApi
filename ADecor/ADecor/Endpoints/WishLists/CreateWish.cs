using ADecor.Endpoints.Products.Commands;
using ADecor.Endpoints.Products;
using Ardalis.ApiEndpoints;
using ADecor.Endpoints.WishLists.Commands;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using ADecor.Endpoints.WishLists.Queries;
using ADecor.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace ADecor.Endpoints.WishLists
{
    [Authorize]
    public class CreateWish : EndpointBaseAsync
        .WithRequest<CreateWishCommand>
        .WithActionResult<WishModel>
    {
        private readonly IMediator mediator;

        public CreateWish(IMediator mediator)
        {

            this.mediator = mediator;
        }
        [HttpPost("wishes/create")]
        [Authorize]
        [SwaggerOperation(Summary = "Create Wish",
          Description = "Insert attributes for a new Wish",
          OperationId = "WishList.Create",
          Tags = new[] { "WishListEndpoint" })]

        public override async Task<ActionResult<WishModel>> HandleAsync(CreateWishCommand request, CancellationToken cancellationToken = default)
        {
             var newWish = await mediator.Send(request);
             return Ok(newWish);
           
        }
    }
}
