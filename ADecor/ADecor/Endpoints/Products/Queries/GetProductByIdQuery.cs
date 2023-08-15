using ADecor.Data;
using ADecor.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ADecor.Endpoints.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int ProductId { get; set; }
    }
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly ADecorContext ctx;

        public GetProductByIdHandler(ADecorContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var prod = await ctx.Product
                .Include(p => p.Brand)
                .Include(p => p.Categories)
                .FirstAsync(x => x.ProductId == request.ProductId, cancellationToken: cancellationToken);
            return prod;
        }
    }
}
