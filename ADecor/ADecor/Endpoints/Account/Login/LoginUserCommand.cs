using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ADecor.Endpoints.Account.Login
{
    public record LoginUserCommand(string Email, string Password) : IRequest<SignInResult>;

    public class LoginUserHandler : IRequestHandler<LoginUserCommand, SignInResult>
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginUserHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);
            return result;
        }
    }
}
