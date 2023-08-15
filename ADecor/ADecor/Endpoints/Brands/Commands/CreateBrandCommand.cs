using ADecor.Data;
using ADecor.Entities;
using MediatR;

namespace ADecor.Endpoints.Brands.Commands
{ public record CreateBrandCommand(string Name):IRequest<Brand>;
    public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, Brand>
    {
        private readonly ADecorContext ctx;

        public CreateBrandHandler(ADecorContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Brand> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var newBrand = new Brand { Name = request.Name };
            await ctx.Brand.AddAsync(newBrand);
            await ctx.SaveChangesAsync();

            return newBrand;
        }
    }
}
