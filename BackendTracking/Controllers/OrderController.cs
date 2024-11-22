using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Shared.Responses;
using System.Text.Json;
using Tracking.BusinessLogicLayer.Blls;

namespace BackendTracking.Controllers
{
    [ApiController]
    [Route("api/")]
    public class OrderController : ControllerBase
    {
        public OrderController()
        {
        }
        [HttpPost("v1/orders")]
        public async Task<ActionResult> Orders()
        {
            try
            {
                return Ok();
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
        [HttpPost("v1/orders/my-orders")]
        public async Task<ActionResult> MyOrders()
        {
            try
            {
                return Ok();
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
