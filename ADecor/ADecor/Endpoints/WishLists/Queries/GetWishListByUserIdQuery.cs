using ADecor.Data;
using ADecor.Migrations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ADecor.Endpoints.WishLists.Queries
{
    public record GetWishListByUserIdQuery(): IRequest<List<WishModel>>;
    
   
    public class GetWishListByUserIdHandler:IRequestHandler<GetWishListByUserIdQuery, List<WishModel>>
    {
        private readonly ADecorContext ctx;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetWishListByUserIdHandler(ADecorContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            this.ctx = ctx;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<WishModel>> Handle(GetWishListByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var wishList = await ctx.WishList
               .Where(w => w.UserId == userId)
               .Select(w => new WishModel
               {
                   WishId = w.WishId,
                   UserId = w.UserId,
                   Popularity = w.Popularity,
                   ProductId = w.ProductId,
                   UserName = w.UserName,
                   ProductName = w.Product.Name, 
                   Quantity = w.Quantity,
                   Price = w.Price
               })
               .ToListAsync();

            return wishList;
        }
    }
}
