using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesManagement.Shared.DTO;
using SalesManagement.Shared.IRepository;
using SalesManagement.Shared.Models;

namespace SalesManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> Get()
        {
            var orders = await _orderRepository.GetAllOrders();
            if (orders.IsSuccess)
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var json = JsonConvert.SerializeObject(orders.Value, settings);
                return Ok(json);
            }
            else
            {
                return BadRequest(orders.ErrorMessage);
            } 
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            if (order.IsSuccess)
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore 
                };

                var json = JsonConvert.SerializeObject(order.Value, settings);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(json);
            }
            else
            {
                return BadRequest(order.ErrorMessage);
            }
        }
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder(OrderDTO order)
        {
            var response= await _orderRepository.AddOrder(order);
            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateOrder(OrderDTO updatedOrder)
        {
            var response =await _orderRepository.UpdateOrder(updatedOrder);
            if (response.IsSuccess )
            {
                return Ok(response.Value);
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _orderRepository.DeleteOrder(id);
            if (response.IsSuccess)
            {
                return Ok(response.Value);
            }
            return BadRequest(response.ErrorMessage);
        }
    }
}
