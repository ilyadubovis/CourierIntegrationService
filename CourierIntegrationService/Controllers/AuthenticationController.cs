using CourierIntegrationService.Authentication;
using CourierIntegrationService.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CourierIntegrationService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public ActionResult<AuthenticationResponse> Login(AuthenticationForm authentication)
    {
        try
        {
            var token = authenticationService.Authenticate(authentication);
            return Ok(new AuthenticationResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = (int)(token.ValidTo - DateTime.UtcNow).TotalSeconds
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
