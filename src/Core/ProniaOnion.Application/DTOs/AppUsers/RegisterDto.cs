namespace ProniaOnion.Application.DTOs.AppUsers
{
    public record RegisterDto(
        string Name,
        string Surname,
        string UserName,
        string Email,
        string Password
    );
}
