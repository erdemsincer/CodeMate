using AuthService.Dtos;

namespace AuthService.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string?> LoginAsync(LoginDto dto);
        Task<List<Guid>> GetAllUserIdsAsync();

    }
}
