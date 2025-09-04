using System.ComponentModel.DataAnnotations;

namespace GaumataWeb.Models
{
    public class OrderItem
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
