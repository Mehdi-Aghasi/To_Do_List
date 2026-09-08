namespace To_Do_List.Application.Common.Dtos
{
    public record AuthDto
    (
        string Token,
        string UserId,
        string Email
    );
}
