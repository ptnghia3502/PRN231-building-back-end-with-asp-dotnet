using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using Services.Interfaces;
using Services.Service;
using Services.ViewModels;

namespace eStoreAPI.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get a list of all products.
        /// </summary>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        /// <summary>
        /// Get product by product ID.
        /// </summary>
        [Authorize]
        [HttpGet("{id}", Name = "ProductDetails")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Create product.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateView productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Invalid product data");
            }

            await _productService.CreateProduct(productDto);

            return Ok(await _productService.GetProductById(productDto.ProductId));
        }

        /// <summary>
        /// Update product.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ProductUpdateView productDto)
        {
            var existingProduct = await _productService.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }

            await _productService.UpdateProduct(id, productDto);

            return Ok(await _productService.GetProductById(id));
        }

        /// <summary>
        /// Delete product.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProduct = await _productService.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound("Member not found");
            }

            await _productService.Delete(id);

            return Ok("Delete successful");
        }
    }
}
