using ADecor.Endpoints.Account.Register;
using Ardalis.ApiEndpoints;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ADecor.Endpoints.Account
{


    public class CreateUser : EndpointBaseAsync
         .WithRequest<CreateUserCommand>
        .WithResult<IdentityResult>
    {
        private readonly IMediator _mediator;

        public CreateUser(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register User",
           Description = "Insert Email and Password",
           OperationId = "User.Register",
           Tags = new[] { "UserEndpoint" })]
        public override async Task<IdentityResult> HandleAsync(CreateUserCommand request, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(request);
            if (result.Succeeded)
            {
                return result;
            }
            return null;
        }
    }


}
