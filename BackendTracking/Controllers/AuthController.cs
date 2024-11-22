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
    public class AuthController : ControllerBase
    {
        private readonly IAuthBLL _authBLL;
        public AuthController(IAuthBLL authBLL)
        {
            _authBLL = authBLL;
        }
        [EnableRateLimiting("login")]
        [HttpPost("v1/auth/login")]
        public async Task<ActionResult> Login(Guid id)
        {
            try
            {
                var users = await _authBLL.Login(id);
                return Ok(users);
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
