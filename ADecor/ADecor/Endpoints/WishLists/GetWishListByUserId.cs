using ADecor.Endpoints.Products.Queries;
using ADecor.Endpoints.WishLists.Queries;
using ADecor.Entities;
using Ardalis.ApiEndpoints;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ADecor.Endpoints.WishLists
{
    [Authorize]
    public class GetWishListByUserId : EndpointBaseAsync
    .WithoutRequest
      .WithActionResult<List<WishList>>
    {
        private readonly IMediator _mediator;


        public GetWishListByUserId(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("wishes/byuser")]
        [Authorize]
        [SwaggerOperation(Summary = "Get your Whish List ",
            Description = "Get your Whish List",
            OperationId = "WishList.GetByUser",
            Tags = new[] { "WishListEndpoint" })]
        
public override async Task<ActionResult<List<WishList>>> HandleAsync( CancellationToken cancellationToken = default)
        {
            var wishList = await _mediator.Send(new GetWishListByUserIdQuery());

            if(wishList == null)
            {
                return null;
            }
            return Ok(wishList);
        }
    }
}
