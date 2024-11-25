using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Shared.Models.DTOs.Records;
using Shared.Responses;
using System.Text.Json;
using Tracking.BusinessLogicLayer.Blls;

namespace BackendTracking.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBLL _orderBLL;
        public OrderController(IOrderBLL orderBLL)
        {
            _orderBLL = orderBLL;
        }
        
        [HttpGet("v1/orders")]
        public async Task<ActionResult> Orders()
        {
            try
            {
                var orders = await _orderBLL.GetAll();
                return Ok(orders);
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
        [HttpGet("v1/orders/{id}")]
        public async Task<ActionResult> GetOrder(Guid id)
        {
            try
            {
                var orders = await _orderBLL.Get(id);
                return Ok(orders);
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
        [HttpPost("v1/orders")]
        public async Task<ActionResult> Orders(OrderRecord orderRecord)
        {
            try
            {
                var order = await _orderBLL.Create(orderRecord);
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
        [HttpPost("v1/orders/my-orders")]
        public async Task<ActionResult> MyOrders()
        {
            try
            {
                var orders = await _orderBLL.GetMyOrders();
                return Ok(orders);
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
