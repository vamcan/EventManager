using EventManager.Core.Application.User.Login;

namespace EventManager.Core.Application.Auth
{
    public interface ITokenService
    {
        Task<string> BuildTokenAsync( LoginCommand user);
        Task<bool> IsTokenValidAsync( string token);
    }
}
