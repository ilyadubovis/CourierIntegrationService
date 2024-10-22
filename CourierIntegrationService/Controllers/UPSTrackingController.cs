using CourierIntegrationService.Models;
using CourierIntegrationService.Models.UPS;
using CourierIntegrationService.Services.Mappers;
using CourierIntegrationService.Services.Tracking;
using Microsoft.AspNetCore.Mvc;

namespace CourierIntegrationService.Controllers;

[Route("api/Tracking/ups")]
[ApiController]
public class UPSTrackingController(ITrackingService trackingService, UPSTrackingInfoMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<TrackingInfo>> GetTrackingInfo() =>
        Ok(await trackingService.GetTrackingInfo(CourierEnum.DHL));

    [HttpGet("{shipmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Shipment>> GetShipment(Guid shipmentId)
    {
        try
        {
            var shipment = await trackingService.GetShipment(shipmentId);
            if (shipment != default)
            {
                return Ok(shipment);
            }
            else
            {
                return NotFound($"Shipment with id {shipmentId} was ot found.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{trackingNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Shipment>> GetShipmentByTrackingNumber(string trackingNumber)
    {
        try
        {
            var shipment = await trackingService.GetShipment(trackingNumber);
            if (shipment != default)
            {
                return Ok(shipment);
            }
            else
            {
                return NotFound($"Shipment with tracking number {trackingNumber} was ot found.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpsertTrackingInfo([FromBody] UPS_TrackingInfo dhlTrackingInfo)
    {
        try
        {
            var trackingInfo = mapper.Map(dhlTrackingInfo);

            foreach (var shipment in trackingInfo.Shipments)
            {
                await UpsertShipment(shipment);
            }

            return Created();

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{shipmentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteShipment(Guid shipmentId)
    {
        try
        {
            await trackingService.DeleteShipment(shipmentId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private async Task UpsertShipment(Shipment shipment)
    {
        var existingShipment = await trackingService.GetShipment(shipment.TrackingNumber);
        if (existingShipment != default)
        {
            await trackingService.UpdateShipment(existingShipment.ShipmentId, shipment);
        }
        else
        {
            await trackingService.CreateShipment(shipment);
        }
    }
}
