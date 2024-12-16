using CourierIntegrationService.Models.DHL;
using CourierIntegrationService.Services.TibcoEMC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CourierIntegrationService.Controllers;

[Route("api/Notification/dhl")]
[ApiController]
public class DHLNotificationController(ITibcoEMCService tibcoEMCService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    public ActionResult ProduceNotificationMessage([FromBody] DHL_TimestampNotification dhlTimestampNotification)
    {
        try
        {
            var jsonString = JsonSerializer.Serialize(dhlTimestampNotification);
            tibcoEMCService.ProduceMessage(jsonString);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
