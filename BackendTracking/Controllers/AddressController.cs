using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Shared.Responses;
using System.Text.Json;
using Tracking.BusinessLogicLayer.Services;

namespace BackendTracking.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/")]
    public class AddressController : ControllerBase
    {
        private readonly ISearchAddressService _searchAddressService;
        public AddressController(ISearchAddressService searchAddressService)
        {
            _searchAddressService = searchAddressService;
        }


        [HttpPost("v1/address")]
        public async Task<ActionResult> PostAddress(double lat = -23.519686413204617, double lon = -46.59250259399414)
        {
            try
            {
                var order = await _searchAddressService.GetAddress(lat, lon);
                return Ok(order);
            }
            catch (KeyNotFoundException error)
            {
                return NotFound(new Response<string>(StatusCodes.Status404NotFound,
                    JsonSerializer.Serialize(new
                    {
                        error.Message,
                        error.Source
                    })));
            }
            catch (DbUpdateException error)
            {
                return UnprocessableEntity(new Response<string>(StatusCodes.Status422UnprocessableEntity,
                    JsonSerializer.Serialize(new
                    {
                        error.Message,
                        error.Source
                    })));
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<string>(StatusCodes.Status500InternalServerError,
                JsonSerializer.Serialize(new
                {
                    error.Message,
                    error.Source
                })));
            }
        }       
    }
}
