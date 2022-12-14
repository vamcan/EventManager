using EventManager.Core.Application.Auth.Dto;

namespace EventManager.Core.Application.Auth
{
    public interface IJwtFactory
    {
        Task<Token> GenerateEncodedToken(Guid id, string userName);
    }
}
