using System.ComponentModel.DataAnnotations;

namespace GaumataWeb.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MarathiName { get; set; }
        public string Description { get; set; }
        public string MarathiDescription { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Features { get; set; }
        public List<string> MarathiFeatures { get; set; }
        public bool IsAvailable { get; set; } = true;
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Product.Price * Quantity;
    }

    public class Order
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "नाव आवश्यक आहे")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "मोबाइल नंबर आवश्यक आहे")]
        [Phone(ErrorMessage = "योग्य मोबाइल नंबर टाका")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "पत्ता आवश्यक आहे")]
        public string DeliveryAddress { get; set; }

        public List<CartItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
