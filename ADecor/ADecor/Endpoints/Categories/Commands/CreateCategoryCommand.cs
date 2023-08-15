using ADecor.Data;
using ADecor.Entities;
using MediatR;

namespace ADecor.Endpoints.Categories.Commands
{
    //public record CreateCategoryCommand(CategoryModel catModel) : IRequest<Unit>;
    public class CreateCategoryCommand : IRequest<Unit>
    {
        public string CategoryName { get; set; }
    }
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Unit>
    {
        private readonly ADecorContext ctx;

        public CreateCategoryHandler(ADecorContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var cat = new Category
            {

                Name = request.CategoryName,
                
                

            };

            ctx.Category.Add(cat);

            await ctx.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
