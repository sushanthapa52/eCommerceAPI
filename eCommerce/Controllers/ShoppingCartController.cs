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

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    // Implement logic to get the user's shopping cart based on the currently authenticated user
        //    string currentUserId = User.Identity.Name; // Example: assuming the user's ID is stored in the Name claim

        //    // Retrieve the shopping cart from the database based on the user ID
        //    ShoppingCart userShoppingCart = _context.ShoppingCarts
        //        .FirstOrDefault(cart => cart.User == currentUserId);

        //    return Ok(userShoppingCart);
        //}

       

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

        [HttpPost("AddItem/{productId}")]
        public IActionResult AddItem(int productId)
        {
            string currentUserId = User.Identity.Name;

            // Create or retrieve the shopping cart for the current user
            ShoppingCart userShoppingCart = _context.ShoppingCarts
                .FirstOrDefault(cart => cart.User == currentUserId);

            if (userShoppingCart == null)
            {
                userShoppingCart = new ShoppingCart
                {
                    User = currentUserId,
                    Products = new List<Product>()
                };
                _context.ShoppingCarts.Add(userShoppingCart);
            }

            // Retrieve the product from the database
            Product productToAdd = _context.Products.Find(productId);

            if (productToAdd != null)
            {
                userShoppingCart.Products.Add(productToAdd);
                _context.SaveChanges();
                return Ok();
            }

            return NotFound(); // Product not found
        }

        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        {
            string currentUserId = User.Identity.Name;
            ShoppingCart userShoppingCart = _context.ShoppingCarts
                .FirstOrDefault(cart => cart.User == currentUserId);

            if (userShoppingCart == null)
            {
                return Ok(new List<Product>()); // Return an empty list if the cart is not found
            }

            return Ok(userShoppingCart.Products);
        }


        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] Product newProduct)
        {
            if (newProduct != null)
            {
                _context.Products.Add(newProduct);
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest(); // Invalid product data
        }





    }
}
