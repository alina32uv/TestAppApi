using ADecor.Data;
using ADecor.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ADecor.Endpoints.WishLists.Queries
{
    public class GetAllWishListsQuery : IRequest<List<WishList>>
        {

        }
    public class GetAllWishListsHandler : IRequestHandler<GetAllWishListsQuery, List<WishList>>
    {
        private readonly ADecorContext ctx;

        public GetAllWishListsHandler(ADecorContext ctx)
        {
            this.ctx = ctx;
        }
        public async  Task<List<WishList>> Handle(GetAllWishListsQuery request, CancellationToken cancellationToken)
        {
            var list = await ctx.WishList
                .Include(c => c.Product)
                .Include(c => c.Product.Brand)
                 .Include(c => c.Product.Categories)
                .ToListAsync();
            return list;
        }
    }
}
