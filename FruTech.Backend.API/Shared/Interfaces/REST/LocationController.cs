using Microsoft.AspNetCore.Mvc;
using FruTech.Backend.API.Shared.Domain.Services;

namespace FruTech.Backend.API.Shared.Interfaces.REST;

/// <summary>
/// Controller for geolocation operations
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class LocationController : ControllerBase
{
    private readonly IGeoLocationService _geoLocationService;
    private readonly ILogger<LocationController> _logger;

    public LocationController(IGeoLocationService geoLocationService, ILogger<LocationController> logger)
    {
        _geoLocationService = geoLocationService;
        _logger = logger;
    }

    /// <summary>
    /// Gets the user's location based on their IP address
    /// </summary>
    /// <returns>JSON object with region and country</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLocation()
    {
        try
        {
            // Obtener la IP real del usuario
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            _logger.LogInformation("Client IP detected: {IpAddress}", ipAddress);

            if (string.IsNullOrEmpty(ipAddress))
            {
                return StatusCode(500, new { error = "Unable to determine IP address" });
            }

            // Obtener la información de geolocalización
            var locationData = await _geoLocationService.GetLocationAsync(ipAddress);

            if (locationData == null)
            {
                return StatusCode(500, new { error = "Unable to retrieve location information" });
            }

            // Devolver un objeto JSON simple con región y país
            return Ok(new
            {
                region = locationData.Region_Name ?? "Unknown",
                country = locationData.Country_Name ?? "Unknown"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving location information");
            return StatusCode(500, new { error = "An error occurred while retrieving location" });
        }
    }
}

