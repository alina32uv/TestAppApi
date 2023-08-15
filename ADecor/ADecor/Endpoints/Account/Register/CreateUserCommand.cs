using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ADecor.Endpoints.Account.Register
{
    public record CreateUserCommand(string Email, string Password) : IRequest<IdentityResult>;
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, IdentityResult>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public CreateUserHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async  Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            return result;
        }
    }
}
