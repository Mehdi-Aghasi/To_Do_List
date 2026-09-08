using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using To_Do_List.Application.Common;
using To_Do_List.Application.Common.Dtos;
using To_Do_List.Domain.Entities;

namespace To_Do_List.Application.Features.Auth.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, AuthDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public RegisterHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser(request.FirstName, request.LastName)
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Registration failed: {errors}");
            }

            var token = JwtTokenGenerator.GenerateToken(user, _configuration);

            return new AuthDto(token, user.Id, user.Email!);
        }
    }
}