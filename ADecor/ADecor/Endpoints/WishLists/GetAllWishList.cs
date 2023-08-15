using ADecor.Endpoints.Products.Queries;
using ADecor.Endpoints.WishLists.Queries;
using ADecor.Entities;
using Ardalis.ApiEndpoints;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace ADecor.Endpoints.WishLists
{
    [Authorize(Roles = "Manager")]
    public class GetAllWishList : EndpointBaseAsync
         .WithoutRequest
         .WithActionResult<List<WishList>>
    {
        private readonly IMediator mediator;

        public GetAllWishList(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("wishes")]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation(Summary = "Get list of wishes",
           Description = "Get all wishes",
           OperationId = "WishList.GetAll",
           Tags = new[] { "WishListEndpoint" })]
        public override async Task<ActionResult<List<WishList>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var list = await mediator.Send(new GetAllWishListsQuery());

            return Ok(list);
        }
    }
}
