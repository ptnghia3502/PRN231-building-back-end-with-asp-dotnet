using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Service;
using Services.ViewModels;

namespace eStoreAPI.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get a list of all orders.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrders();

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        /// <summary>
        /// Get order by order ID.
        /// </summary>
        [HttpGet("{id}", Name = "OrderDetails")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrdertById(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        /// <summary>
        /// Get a list of all orders by member ID.
        /// </summary>
        [HttpGet("{memberid}/orders-detail", Name = "OrdersOfMember")]
        public async Task<IActionResult> OrdersOfMember(int memberid)
        {
            if (memberid < 0)
            {
                return NotFound();
            }

            var orders = await _orderService.GetOrderstByMemberId(memberid); 

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        /// <summary>
        /// Create order.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateView orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest("Invalid order data");
            }

            await _orderService.CreateOrder(orderDto);

            return Ok(await _orderService.GetOrdertById(orderDto.OrderId));
        }

        /// <summary>
        /// Update order.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] OrderUpdateView orderDto)
        {
            var existingOrder = await _orderService.GetOrdertById(id);
            if (existingOrder == null)
            {
                return NotFound("Order not found");
            }

            await _orderService.UpdateOrder(id, orderDto);

            return Ok(await _orderService.GetOrdertById(id));
        }

        /// <summary>
        /// Delete order.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingOrder = await _orderService.GetOrdertById(id);
            if (existingOrder == null)
            {
                return NotFound("Order not found");
            }

            await _orderService.Delete(id);

            return Ok("Delete successful");
        }
    }
}
