using CourierIntegrationService.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourierIntegrationService.Services.Authentication;

public class AuthenticationService(IOptions<AuthenticationOptions> options) : IAuthenticationService
{
    public JwtSecurityToken Authenticate(AuthenticationForm authentication)
    {
        if (authentication == null)
        {
            throw new Exception("Login information cannot be blank.");
        }
        if (authentication.GrantType != "client_credentials")
        {
            throw new Exception($"Grant Type '{authentication.GrantType}' is invalid. Please use 'client_credentials'.");
        }
        if (authentication.ClientId != options.Value.ClientId)
        {
            throw new Exception("Client Id is invalid.");
        }
        if (authentication.ClientSecret != options.Value.ClientSecret)
        {
            throw new Exception("Client Secret is invalid.");
        }

        var claims = new List<Claim>
        {
            // we may add specific claims in the future
            new(ClaimTypes.Sid, Guid.NewGuid().ToString())
        };

        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.Value.SecretKey));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var tokenExpirationDate = DateTimeOffset.UtcNow.AddHours(1);

        return new JwtSecurityToken(
            claims: claims,
            expires: tokenExpirationDate.UtcDateTime,
            signingCredentials: credentials);
    }
}
