using ADecor.Data;
using ADecor.Endpoints.Brands.Queries;
using ADecor.Endpoints.Categories.Query;
using ADecor.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ADecor.Endpoints.Categories.Commands
{
   /* public record UpdateCategoryCommand(int CategoryId, string Name) : IRequest<CategoryModel>;
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryModel>
    {
        private readonly ADecorContext ctx;
        private readonly IMediator mediator;

        public UpdateCategoryHandler(ADecorContext ctx, IMediator mediator)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }
        public async Task<CategoryModel> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {

            var existingCat =  await mediator.Send(new GetCategoryByIdQuery { CategoryId = request.CategoryId });

            if (existingCat == null)
            {

            }
            existingCat.Name = request.Name;
            //ctx.Entry(existingCar).State = EntityState.Modified;
            ctx.Category.Update(existingCat);
            await ctx.SaveChangesAsync();

            return existingCat;
        }*/
    


}
