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
    public class DrivingController : ControllerBase
    {
        private readonly IDrivingService _drivingService;
        public DrivingController(IDrivingService drivingService)
        {
            _drivingService = drivingService;
        }

        [HttpGet("v1/driving")]
        public async Task<ActionResult> GetRoute(PositionResponse orderRecord)
        {
            try
            {
                var order = await _drivingService.GetRoute(orderRecord);
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

        [HttpPost("v1/driving")]
        public async Task<ActionResult> PostRoute(PositionResponse orderRecord)
        {
            try
            {
                var order = await _drivingService.GetRoute(orderRecord);
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
