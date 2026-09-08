using MediatR;
using To_Do_List.Application.Common.Dtos;

namespace To_Do_List.Application.Features.Auth.Commands.Login
{
    public record LoginCommand
    (
        string Email,
        string Password
    ) : IRequest<AuthDto>;
}