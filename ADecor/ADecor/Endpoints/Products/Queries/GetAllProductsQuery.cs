using ADecor.Data;
using ADecor.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ADecor.Endpoints.Products.Queries
{
    public class ProductWithCategories
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public string Image { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<string> CategoryNames { get; set; }
    }
    public class GetAllProductsQuery : IRequest<List<ProductWithCategories>>
        {

        }
        public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductWithCategories>>
        {
            private readonly ADecorContext ctx;

            public GetAllProductsHandler(ADecorContext ctx)
            {
                this.ctx = ctx;
            }


         

        public async Task<List<ProductWithCategories>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productsWithCategories = await ctx.Product
                 .Include(p => p.Brand)
                 .Include(p => p.ProductCategories)
                 .ThenInclude(pc => pc.Category)
                 .Select(p => new ProductWithCategories
                 {
                     ProductId = p.ProductId,
                     Name = p.Name,
                     Price = p.Price,
                     Color = p.Color,
                     Height = p.Height,
                     Width = p.Width,
                     Image = p.Image,
                     BrandId = p.BrandId,
                     Brand = p.Brand,
                     CategoryNames = p.ProductCategories.Select(pc => pc.Category.Name).ToList()
                 })
                 .ToListAsync();

            return productsWithCategories;
        }
    }
    
}
