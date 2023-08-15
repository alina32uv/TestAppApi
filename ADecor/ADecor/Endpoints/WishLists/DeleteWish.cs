using ADecor.Endpoints.Products.Commands;
using ADecor.Endpoints.Products.Queries;
using ADecor.Endpoints.WishLists.Commands;
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
    public class DeleteWish : EndpointBaseAsync
   .WithRequest<int>
      .WithActionResult<WishList>
    {
        private readonly IMediator _mediator;

        public DeleteWish(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpDelete("wishes/{id:int}")]
        [Authorize]
        [SwaggerOperation(Summary = "Delete item from Wish List by id",
          Description = "Input an int value to delete item by its Id from WishList",
          OperationId = "WishList.Delete",
          Tags = new[] { "WishListEndpoint" })]
        public override async Task<ActionResult<WishList>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var wish = await _mediator.Send(new GetWishListByIdQuery { WishId = id });
            if (wish != null)
            {
                await _mediator.Send(new DeleteWishCommand { WishId = id });
            }


            return wish;
        }
    }
}
