using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        //function to return all the products
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var allProducts = _context.Products.ToList();
            return Ok(allProducts);
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetProductsByCategory(int categoryId)
        {
            try
            {
                // Retrieve the category with associated products
                var categoryWithProducts = _context.Categories
                    .Include(c => c.Products)
                    .FirstOrDefault(c => c.Id == categoryId);

                if (categoryWithProducts == null)
                {
                    return NotFound("Category not found");
                }

                // Extract the products from the category
                var products = categoryWithProducts.Products;

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }







        // get product by id
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound(); // Product not found
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if the associated category exists or create it if not
                    var category = _context.Categories.Find(product.CategoryId);
                    if (category == null)
                    {
                        // Create a new category
                        category = new Category { Id = product.CategoryId, Description = "Default Category" };
                        _context.Categories.Add(category);
                    }

                    // Associate the product with the category
                    product.Category = category;

                    // Add the product to the database
                    _context.Products.Add(product);
                    _context.SaveChanges();

                    return Ok("Product added successfully");
                }

                return BadRequest("Invalid product data");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






    }
}
