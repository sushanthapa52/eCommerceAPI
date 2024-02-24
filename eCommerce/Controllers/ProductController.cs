using eCommerce.Data;
using eCommerceClassLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly AppDataContext _context;


        public ProductController(ILogger<ProductController> logger, AppDataContext context)
        {
            _logger = logger;
            _context = context;
        }
        // Add a Get endpoint that returns all products.	
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var allProducts = await _context.Products.ToListAsync();
                return Ok(allProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Add a Get endpoint that takes a category Id and returns all products in that category.	
        [HttpGet("retrieveProductsByCategory/{categoryId}")]
        public async Task<IActionResult> RetrieveProductsByCategory(int categoryId)
        {
            try
            {
                var productsInCategory = await _context.Products
                    .Where(product => product.ProductCategory.Id == categoryId)
                    .ToListAsync();

                return Ok(productsInCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Add a Post endpoint that takes a single product and adds it to the database.	
        [HttpPost("addProductToDB")]
        public async Task<IActionResult> AddProduct([FromBody] Product newProduct)
        {
            try
            {
                if (newProduct == null)
                {
                    return BadRequest("Invalid product data");
                }

                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();

                var allProducts = await _context.Products.ToListAsync();
                return Ok(allProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
