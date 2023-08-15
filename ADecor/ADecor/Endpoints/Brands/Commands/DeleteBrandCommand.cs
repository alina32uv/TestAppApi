using ADecor.Data;
using ADecor.Endpoints.Brands.Queries;
using ADecor.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ADecor.Endpoints.Brands.Commands
{
    public class DeleteBrandCommand : IRequest<Brand>
    {
        public int BrandId { get; set; }
    }
    public class DeleteBrandHandler : IRequestHandler<DeleteBrandCommand, Brand>
    {
        private readonly ADecorContext ctx;
        private readonly IMediator mediator;

        public DeleteBrandHandler(ADecorContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Brand> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var existingBrand = await mediator.Send(new GetBrandQuery { BrandId = request.BrandId });
            if (existingBrand != null)
            {

                ctx.Brand.Remove(existingBrand);
                await ctx.SaveChangesAsync();
            }
            return existingBrand;
        }
    }
}
