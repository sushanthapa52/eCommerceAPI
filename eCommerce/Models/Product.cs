namespace eCommerce.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }

        public decimal Price { get; set; }

        // Adding a Category property to the Product model
        public Category ProductCategory { get; set; }




    }
}
