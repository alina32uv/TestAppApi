using ADecor.Data;
using ADecor.Endpoints.Products.Queries;
using ADecor.Endpoints.WishLists.Queries;
using ADecor.Entities;
using MediatR;

namespace ADecor.Endpoints.WishLists.Commands
{
    public class DeleteWishCommand:IRequest<WishList>
    { public int WishId { get; set; }
    }
    public class DeleteWishHandler : IRequestHandler<DeleteWishCommand, WishList>
    {
        private readonly ADecorContext ctx;
        private readonly IMediator mediator;

        public DeleteWishHandler(ADecorContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;

        }
        public async Task<WishList> Handle(DeleteWishCommand request, CancellationToken cancellationToken)
        {
            var existingWish = await mediator.Send(new GetWishListByIdQuery { WishId = request.WishId });
            if (existingWish != null)
            {

                ctx.WishList.Remove(existingWish);
                await ctx.SaveChangesAsync();
            }
            return existingWish;
        }
    }
}
