using EventManager.Core.Application.Auth.Dto;

namespace EventManager.Core.Application.Auth
{
    public interface ITokenFactory
    {
        Task<Token> GenerateEncodedToken(Guid id, string userName);
    }
}
