using eCommerceClassLib.Models;

namespace eCommerceTest
{
    [TestClass]
    public class TestLogic
    {
        #region test for product
        // test added for the product model

        [TestMethod]
        public void Product_Id_Should_Be_Set()
        {
            // Arrange
            var product = new Product();

            // Act

            // Assert
            Assert.AreEqual(0, product.Id); // Assuming default value is 0
        }

        [TestMethod]
        public void Product_Name_Should_Be_Set()
        {
            // Arrange
            var product = new Product();

            // Act
            product.Name = "Bose headset";

            // Assert
            Assert.AreEqual("Bose headset", product.Name);
        }

        [TestMethod]
        public void Product_Description_Should_Be_Set()
        {
            // Arrange
            var product = new Product();

            // Act
            product.Description = "Soundlink noise cancellation";

            // Assert
            Assert.AreEqual("Soundlink noise cancellation", product.Description);
        }

        [TestMethod]
        public void Product_Price_Should_Be_Set()
        {
            // Arrange
            var product = new Product();

            // Act
            product.Price = 19.99m;

            // Assert
            Assert.AreEqual(19.99m, product.Price);
        }
        #endregion


        #region test for category
        //test added for the Category model

        [TestMethod]
        public void Category_Id_Should_Be_Set()
        {
            // Arrange
            var category = new Category();

            // Act

            // Assert
            Assert.AreEqual(0, category.Id); // Assuming default value is 0
        }

        [TestMethod]
        public void Category_Description_Should_Be_Set()
        {
            // Arrange
            var category = new Category();

            // Act
            category.Description = "Test Category";

            // Assert
            Assert.AreEqual("Test Category", category.Description);
        }

        [TestMethod]
        public void Category_Description_Can_Be_Null()
        {
            // Arrange
            var category = new Category();

            // Act
            category.Description = null;

            // Assert
            Assert.IsNull(category.Description);
        }

        [TestMethod]
        public void Category_Id_Should_Not_Be_Negative()
        {
            // Arrange
            var category = new Category();

            // Act
            category.Id = -1;

            // Assert
            Assert.IsTrue(category.Id >= 0);
        }

        #endregion


        #region test for shopping cart

        [TestMethod]
        public void ShoppingCart_Id_Should_Be_Set()
        {
            // Arrange
            var shoppingCart = new ShoppingCart();

            // Act

            // Assert
            Assert.AreEqual(0, shoppingCart.Id); // Assuming default value is 0
        }

        [TestMethod]
        public void ShoppingCart_User_Should_Be_Set()
        {
            // Arrange
            var shoppingCart = new ShoppingCart();

            // Act
            shoppingCart.User = "TestUser";

            // Assert
            Assert.AreEqual("TestUser", shoppingCart.User);
        }

        [TestMethod]
        public void ShoppingCart_Products_Should_Not_Be_Null()
        {
            // Arrange
            var shoppingCart = new ShoppingCart();

            // Act

            // Assert
            Assert.IsNotNull(shoppingCart.Products);
        }

        [TestMethod]
        public void ShoppingCart_AddProduct_Should_Increase_Products_Count()
        {
            // Arrange
            var shoppingCart = new ShoppingCart();
            var product = new Product();

            // Act
            shoppingCart.Products.Add(product);

            // Assert
            Assert.AreEqual(1, shoppingCart.Products.Count);
        }

        #endregion
    }
}


