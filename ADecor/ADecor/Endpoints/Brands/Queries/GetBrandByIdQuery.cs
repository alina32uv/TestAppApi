using ADecor.Data;
using ADecor.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ADecor.Endpoints.Brands.Queries
{
    public class GetBrandQuery : IRequest<Brand>
    {
        public int BrandId { get; set; }
    }

    public class GetBrandQueryHandler : IRequestHandler<GetBrandQuery, Brand>
    {
        private readonly ADecorContext ctx;

        public GetBrandQueryHandler(ADecorContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<Brand> Handle(GetBrandQuery request, CancellationToken cancellationToken)
        {
            var brand = await ctx.Brand.FirstAsync(x => x.BrandId == request.BrandId, cancellationToken: cancellationToken);
            return brand;
        }
    }


    }

