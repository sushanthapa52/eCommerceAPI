﻿namespace eCommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Description { get; set; }

        // Navigation property
        public List<Product> Products { get; set; }

    }
}
