using eCommerce.Data;
using eCommerceClassLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                string currentUserName = User.Identity.Name;

                // Retrieve the user's shopping cart with included products
                var userShoppingCart = await _context.ShoppingCarts
                    .Include(cart => cart.Products) // Include related products
                    .Where(cart => cart.User == currentUserName)
                    .FirstOrDefaultAsync();

                if (userShoppingCart == null)
                {
                    return NotFound("Shopping cart not found for the user.");
                }

                // Extract the products from the shopping cart
                var products = userShoppingCart.Products;

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Add a Post endpoint that takes a single ID and removes the item from the shopping cart.	
        [HttpPost("RemoveProduct/{productId}")]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            try
            {
                string currentUserName = User.Identity.Name;

                var userShoppingCart = await _context.ShoppingCarts
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(cart => cart.User == currentUserName);

                if (userShoppingCart != null)
                {
                    var productToRemove = userShoppingCart.Products.FirstOrDefault(p => p.Id == productId);

                    if (productToRemove != null)
                    {
                        userShoppingCart.Products.Remove(productToRemove);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                }

                return NotFound(); // Product or shopping cart not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        //Add a Post endpoint that takes a single ID and adds the item to the shopping cart. Make sure to create the Shopping Cart if needed and Assign the Current Users Email to the User property.
        [HttpPost("AddToCart/{productId}")]
        public async Task<IActionResult> AddToCart(int productId)
        {
            try
            {
                string currentUserName = User.Identity.Name;

                // Retrieve or create the user's shopping cart
                var userShoppingCart = await _context.ShoppingCarts
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(cart => cart.User == currentUserName);

                if (userShoppingCart == null)
                {
                    // Create a new shopping cart for the user
                    userShoppingCart = new ShoppingCart
                    {
                        User = currentUserName,
                        Products = new List<Product>()
                    };
                    _context.ShoppingCarts.Add(userShoppingCart);
                }

                // Retrieve the product to add to the cart
                var productToAdd = await _context.Products.FindAsync(productId);

                if (productToAdd != null)
                {
                    // Add the product to the user's shopping cart
                    userShoppingCart.Products.Add(productToAdd);
                    await _context.SaveChangesAsync();

                    return Ok();
                }

                return NotFound(); // Product not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }


}
