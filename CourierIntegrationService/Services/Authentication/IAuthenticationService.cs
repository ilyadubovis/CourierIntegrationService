using CourierIntegrationService.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace CourierIntegrationService.Services.Authentication;

public interface IAuthenticationService
{
    public JwtSecurityToken Authenticate(AuthenticationForm authentication);
}
