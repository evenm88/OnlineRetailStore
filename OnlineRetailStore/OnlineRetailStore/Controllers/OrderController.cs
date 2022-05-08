using Microsoft.AspNetCore.Mvc;
using OnlineRetailStoreApi.Models;
using OnlineRetailStoreApi.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace OnlineRetailStoreApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService orderservice;

        public OrderController(IOrderService orderservice)
        {
            this.orderservice = orderservice;
        }

        // GET: OrderController
        [HttpGet]
        public ActionResult<List<Order>> GetOrderList()
        {
            return orderservice.GetOrderList();
        }

        [HttpGet("{orderId}")]
        public IActionResult GetOrder(string orderId)
        {
            var order = orderservice.GetOrder(orderId);
            if (order == null)
            {
                return NotFound("No match found");
            }

            return Ok(order);
        }

        // GET: OrderController/Delete/"5"
        [HttpDelete]
        [Route("[action]/orderId")]
        public IActionResult Delete(string orderId)
        {
            try
            {
                orderservice.DeleteOrder(orderId);
                return Ok("Deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: OrderController/Add/new Order()
        [HttpPost]
        [Route("[action]")]
        public IActionResult Add(Order order)
        {
            try
            {
                orderservice.AddOrder(order);
                return Ok("Added");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
