using ADecor.Data;
using ADecor.Endpoints.Brands.Queries;
using ADecor.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ADecor.Endpoints.Categories.Query
{
    public class GetAllCategoriesQuery : IRequest<List<Category>>
    {

    }
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, List<Category>>
    {
        private readonly ADecorContext ctx;

        public GetAllCategoriesHandler(ADecorContext ctx)
        {
            this.ctx = ctx;
        }
        

        public async Task<List<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var cat = await ctx.Category.ToListAsync();
            return cat;
        }
    }
}
