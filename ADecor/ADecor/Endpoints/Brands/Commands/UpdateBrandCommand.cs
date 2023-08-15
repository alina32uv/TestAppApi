using ADecor.Data;
using ADecor.Endpoints.Brands.Queries;
using ADecor.Entities;
using Ardalis.ApiEndpoints;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace ADecor.Endpoints.Brands.Commands
{

    public class UpdateBrandCommand : IRequest<Brand>
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
    }
    public class UpdateBrandHandler : IRequestHandler<UpdateBrandCommand, Brand>
    {
        private readonly ADecorContext ctx;
        private readonly IMediator mediator;

        public UpdateBrandHandler(ADecorContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<Brand> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var existingBrand = await mediator.Send(new GetBrandQuery { BrandId = request.BrandId });
            if (existingBrand != null)
            { 
                
                existingBrand.Name = request.Name; // Setează noul nume
                ctx.Entry(existingBrand).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
            return existingBrand;
        }
    }
}
