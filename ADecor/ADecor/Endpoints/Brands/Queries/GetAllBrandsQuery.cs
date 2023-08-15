using ADecor.Data;
using ADecor.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ADecor.Endpoints.Brands.Queries
{
    public class GetAllBrandsQuery : IRequest<List<Brand>>
    {

    }

    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, List<Brand>>
    {
        private readonly ADecorContext ctx;

        public GetAllBrandsHandler(ADecorContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<List<Brand>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brand = await ctx.Brand.ToListAsync();
            return brand;
        }
    }
}
