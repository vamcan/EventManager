using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventManager.Core.Application.Auth;
using EventManager.Core.Application.User.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EventManager.Infrastructure.Auth.Jwt
{
    public class JwtService : ITokenService
    {
        public JwtService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const double ExpiryDurationMinutes = 30;
        public Task<string> BuildTokenAsync(LoginCommand user)
        {
            string key = Configuration["Jwt:Key"];
            string issuer = Configuration["Jwt:Issuer"];
            var claims = new[] {
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddMinutes(ExpiryDurationMinutes), signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return Task.FromResult(token);
        }

        public Task<bool> IsTokenValidAsync(string token)
        {
            string key = Configuration["Jwt:Key"];
            string issuer = Configuration["Jwt:Issuer"];
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}
