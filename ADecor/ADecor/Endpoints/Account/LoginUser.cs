using ADecor.Endpoints.Account.Login;
using Ardalis.ApiEndpoints;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ADecor.Endpoints.Account
{
    public class LoginResponse
    {
        public Microsoft.AspNetCore.Identity.SignInResult SignInResult { get; set; }
        public string Token { get; set; }
    }
    public class LoginUser : EndpointBaseAsync
        .WithRequest<LoginUserCommand>.
        WithResult<LoginResponse>
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;


        public LoginUser(IMediator mediator, SignInManager<IdentityUser> signInManager, IConfiguration config )
        {
            _mediator = mediator;
            _signInManager = signInManager;
            _config = config;
           
        }
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Login User",
           Description = "Insert Email and Password",
           OperationId = "User.Login",
           Tags = new[] { "UserEndpoint" })]
        public override  async Task<LoginResponse> HandleAsync(LoginUserCommand request, CancellationToken cancellationToken = default)
        {
            //var result = await _mediator.Send(request);
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);
            string tokenString = null;
            if (result.Succeeded)
            {
                var user = await _signInManager.UserManager.FindByEmailAsync(request.Email);
                var roles = await _signInManager.UserManager.GetRolesAsync(user); 

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(ClaimTypes.Role, string.Join(",", roles)),

                };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials);

                 tokenString = new JwtSecurityTokenHandler().WriteToken(token);


                //return tokenString;
            }
            var response = new LoginResponse
            {
                SignInResult = result,
                Token = tokenString
            };

            return response;
            //return result ;
        }
    }
}
