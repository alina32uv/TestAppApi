using ADecor.Data;
using ADecor.Endpoints.Categories;
using ADecor.Entities;
using MediatR;

namespace ADecor.Endpoints.Products.Commands
{
    public record CreateProductCommand(ProductModel prodModel) : IRequest<Unit>;
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Unit>
    {
        private readonly ADecorContext ctx;

        public CreateProductHandler(ADecorContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var prod = new Product
            {

                Name = request.prodModel.Name,
                Price = request.prodModel.Price,
                Color = request.prodModel.Color,
                Image = request.prodModel.Image,
                Height = request.prodModel.Height,
                Width = request.prodModel.Width,
                BrandId = request.prodModel.BrandId
               



            };

            ctx.Product.Add(prod);

            await ctx.SaveChangesAsync();
            if (request.prodModel.CategoryIds != null && request.prodModel.CategoryIds.Any())
            {
                foreach (var categoryId in request.prodModel.CategoryIds)
                {
                    var category = await ctx.Category.FindAsync(categoryId);
                    if (category != null)
                    {
                        ctx.ProductCategories.Add(new ProductCategory { ProductId = prod.ProductId, CategoryId = category.CategoryId });
                    }
                }
                await ctx.SaveChangesAsync();
            }
            return Unit.Value;
        }
    }
}
