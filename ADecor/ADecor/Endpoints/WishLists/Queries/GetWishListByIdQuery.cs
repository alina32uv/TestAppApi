using ADecor.Data;
using ADecor.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ADecor.Endpoints.WishLists.Queries
{
    public class GetWishListByIdQuery : IRequest<WishList>
    { public int WishId { get; set; }
    }
    public class GetWishListByIdHandler : IRequestHandler<GetWishListByIdQuery, WishList>
    {
        private readonly ADecorContext ctx;

        public GetWishListByIdHandler(ADecorContext ctx)
        {
            this.ctx = ctx;
            
        }

        public async Task<WishList> Handle(GetWishListByIdQuery request, CancellationToken cancellationToken)
        {
            var orderFromDb = await ctx.WishList
         .Include(c => c.Product)
                .Include(c => c.Product.Brand)
                 .Include(c => c.Product.Categories)
         .FirstOrDefaultAsync(c => c.WishId == request.WishId);

            if (orderFromDb != null)
            {
                return orderFromDb;
            }
            return null;
        }
    }

}
