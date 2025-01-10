using ProniaOnion.Application.DTOs.AppUsers;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(RegisterDto userDto);
        Task LoginAsync(LoginDto userDto);
    }
}
