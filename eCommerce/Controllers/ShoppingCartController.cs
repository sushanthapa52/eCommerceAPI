using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly AppDataContext _context;

        public ShoppingCartController(ILogger<ShoppingCartController> logger, AppDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        //Add a Get endpoint that returns all products in the user's shopping cart.	
        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        {
            string currentUserId = User.Identity.Name;
            ShoppingCart userShoppingCart = _context.ShoppingCarts
                .FirstOrDefault(cart => cart.User == currentUserId);

            if (userShoppingCart == null)
            {
                return Ok(new List<Product>());
            }

            return Ok(userShoppingCart.Products);
        }

        //Add a Post endpoint that takes a single ID and removes the item from the shopping cart.	
        [HttpPost("RemoveItem/{productId}")]
        public IActionResult RemoveItem(int productId)
        {
            string currentUserId = User.Identity.Name;
            ShoppingCart userShoppingCart = _context.ShoppingCarts
                .FirstOrDefault(cart => cart.User == currentUserId);

            if (userShoppingCart != null)
            {
                // Assuming you have a method to remove an item from the shopping cart
                var productToRemove = userShoppingCart.Products.FirstOrDefault(p => p.Id == productId);
                if (productToRemove != null)
                {
                    userShoppingCart.Products.Remove(productToRemove);
                    _context.SaveChanges();
                    return Ok();
                }
            }

            return NotFound(); // Product or shopping cart not found
        }

        // PUT api/shoppingcart/additem/{userId}
        [HttpPut("additem/{userId}")]
        public IActionResult AddItemToCart([FromBody] Product productRequest, string userId)
        {
            // Find or create the shopping cart
            var shoppingCart = _context.ShoppingCarts.FirstOrDefault(cart => cart.User == userId);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    Id = shoppingCart.Id + 1, // Generate a new ID (replace with your logic)
                    User = userId,
                    Products = new List<Product>()
                };

                _context.ShoppingCarts.Add(shoppingCart);
            }

            // Check if the product is already in the cart
            var existingProduct = shoppingCart.Products.FirstOrDefault(product => product.Id == productRequest.Id);

            if (existingProduct != null)
            {
                return BadRequest("Product is already in the shopping cart");
            }

            // Add the product to the cart using data from the request
            var newProduct = new Product
            {
                Id = productRequest.Id,
                Name = productRequest.Name,
                Price = productRequest.Price
               
            };

            shoppingCart.Products.Add(newProduct);

            return Ok($"Product with ID {productRequest.Id} added to the shopping cart");
        }
    }


 }
