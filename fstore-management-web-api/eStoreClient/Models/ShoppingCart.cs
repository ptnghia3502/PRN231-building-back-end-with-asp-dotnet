using Repositories.Models;

namespace eStoreClient.Models
{
    public class ShoppingCart
    {
        private readonly List<CartItem> _cartItems = new List<CartItem>();

        public IReadOnlyList<CartItem> CartItems => _cartItems.AsReadOnly();

        public void AddToCart(Product product, int quantity)
        {
            var existingItem = _cartItems.FirstOrDefault(item => item.Product.ProductId == product.ProductId);

            if (existingItem != null)
            {
                // If the product is already in the cart, update the quantity.
                existingItem.Quantity += quantity;
            }
            else
            {
                // If the product is not in the cart, add it as a new item.
                var newItem = new CartItem
                {
                    Product = product,
                    Quantity = quantity
                };
                _cartItems.Add(newItem);
            }
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var existingItem = _cartItems.FirstOrDefault(item => item.Product.ProductId == productId);

            if (existingItem != null)
            {
                // Update the quantity of the existing item in the cart.
                existingItem.Quantity = quantity;
            }
        }

        public void RemoveFromCart(int productId)
        {
            var itemToRemove = _cartItems.FirstOrDefault(item => item.Product.ProductId == productId);

            if (itemToRemove != null)
            {
                // Remove the item from the cart.
                _cartItems.Remove(itemToRemove);
            }
        }

        public void ClearCart()
        {
            _cartItems.Clear();
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

}
