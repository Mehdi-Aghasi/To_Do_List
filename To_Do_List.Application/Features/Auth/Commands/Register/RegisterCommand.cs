using MediatR;
using To_Do_List.Application.Common.Dtos;

namespace To_Do_List.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand
    (
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<AuthDto>;
}