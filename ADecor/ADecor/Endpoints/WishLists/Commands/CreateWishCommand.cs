using ADecor.Data;
using ADecor.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ADecor.Endpoints.WishLists.Commands
{
    public record CreateWishCommand(int ProductId, int Quantity) : IRequest<Unit>;
    public class CreateWishHandler : IRequestHandler<CreateWishCommand, Unit>
    {
        private readonly ADecorContext ctx;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateWishHandler(ADecorContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            this.ctx = ctx;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(CreateWishCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = await GetUserNameAsync(userId);
           /* var product = await ctx.Product.FirstOrDefaultAsync(p => p.ProductId == request.wish.ProductId);

            if (product == null)
            {
                // Produsul nu există, ar trebui să gestionați această situație corespunzător
                throw new Exception("Product not found.");
            }*/
            //decimal calculatedPrice = (decimal)(request.wish.Quantity * product.Price);

            var _wish = new WishList
            {

                UserId = userId,
                ProductId = request.ProductId,
                UserName = userName,
                Quantity = request.Quantity,
                Price = CalculatePrice(request.ProductId, request.Quantity),
                Popularity = CalculatePopularity(request.ProductId)
                




            };

            ctx.WishList.Add(_wish);

            try
            {
                await ctx.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Afișați informații despre excepție pentru a investiga problema
                Console.WriteLine($"DbUpdateException: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                // Puteți adăuga și alte detalii despre excepție sau folosiți ex.StackTrace pentru a obține stack trace-ul
            }

            return Unit.Value;
        }
        private async Task<string> GetUserNameAsync(string userId)
        {
            var userManager = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
            var user = await userManager.FindByIdAsync(userId);

            return user?.UserName;
        }
        private decimal CalculatePrice(int productId, int quantity)
        {
            var product = ctx.Product.FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                
                throw new Exception("Product not found.");
            }

            return (decimal)(product.Price * quantity);
        }

        private int CalculatePopularity(int productId)
        {
            var popularity = ctx.WishList.Count(w => w.ProductId == productId);

    return popularity;
        }
    }
}
