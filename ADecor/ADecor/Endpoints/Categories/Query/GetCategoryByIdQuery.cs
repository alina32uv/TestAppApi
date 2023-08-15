using ADecor.Data;
using ADecor.Endpoints.Brands.Queries;
using ADecor.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ADecor.Endpoints.Categories.Query
{
    public class GetCategoryByIdQuery :IRequest<Category>
    {
        public int CategoryId { get; set; }
    }
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly ADecorContext ctx;

        public GetCategoryByIdHandler(ADecorContext ctx)
        {
            this.ctx = ctx;
        }

      
        
        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var cat = await ctx.Category.FirstAsync(x => x.CategoryId == request.CategoryId, cancellationToken: cancellationToken);
            return cat;
        }
    }
}
