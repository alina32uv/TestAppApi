using ADecor.Data;
using ADecor.Endpoints.Brands.Commands;
using ADecor.Endpoints.Brands.Queries;
using ADecor.Endpoints.Products.Queries;
using ADecor.Entities;
using MediatR;

namespace ADecor.Endpoints.Products.Commands
{
    public class  DeleteProductCommand: IRequest<Product>
    {
        public int ProductId { get; set; }
}
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Product>
    {
        private readonly ADecorContext ctx;
        private readonly IMediator mediator;

        public DeleteProductHandler(ADecorContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Product> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var existingProd = await mediator.Send(new GetProductByIdQuery { ProductId = request.ProductId });
            if (existingProd != null)
            {

                ctx.Product.Remove(existingProd);
                await ctx.SaveChangesAsync();
            }
            return existingProd;
        }
    }
}
