using ADecor.Data;
using ADecor.Endpoints.Brands.Queries;
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
    public class GetWishListById : EndpointBaseAsync
    .WithRequest<int>
      .WithActionResult<List<WishList>>
    {
        private readonly IMediator _mediator;


        public GetWishListById(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("wishes/byuser/{id:int}")]
        [Authorize(Roles = "Manager")]
        [SwaggerOperation(Summary = "Get WishList By Id ",
            Description = "Input wish List for details ",
            OperationId = "WishList.GetById",
            Tags = new[] { "WishListEndpoint" })]
        public override async  Task<ActionResult<List<WishList>>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var wishList = await _mediator.Send(new GetWishListByIdQuery { WishId = id });

            if (wishList == null)
            {
                return null;
            }
            return Ok(wishList);
        }
    }
}
