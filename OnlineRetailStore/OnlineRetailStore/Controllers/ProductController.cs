using Microsoft.AspNetCore.Mvc;
using OnlineRetailStoreApi.Models;
using OnlineRetailStoreApi.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace OnlineRetailStoreApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET: ProductController
        [HttpGet]
        public ActionResult<List<Product>> GetProductList()
        {
            return productService.GetProductList();
        }

        [HttpGet("{productId}")]
        public IActionResult GetProduct(string productId)
        {
            var order = productService.GetProduct(productId);
            if (order == null)
            {
                return NotFound("No match found");
            }

            return Ok(order);
        }

        // GET: ProductController/Delete/"5"
        [HttpDelete]
        [Route("[action]/productId")]
        public IActionResult Delete(string productId)
        {
            try
            {
                productService.DeleteProduct(productId);
                return Ok("Deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: ProductController/Add/new product()
        [HttpPost]
        [Route("[action]")]
        public IActionResult Add(Product product)
        {
            try
            {
                productService.AddProduct(product);
                return Ok("Added");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
