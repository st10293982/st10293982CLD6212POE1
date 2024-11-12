namespace st10293982CLD6212POE1.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Calculate total price for this cart item
        public decimal Total => Price * Quantity;
    }

    // Cart model that holds all CartItems
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        // Add an item to the cart
        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }

        // Remove an item from the cart
        public void RemoveItem(int productId)
        {
            var item = Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        // Calculate the total price of the cart
        public decimal TotalPrice => Items.Sum(i => i.Total);
    }
}
