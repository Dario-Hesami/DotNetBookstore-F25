using System.ComponentModel.DataAnnotations;

namespace DotNetBookstore.Models
{
    public class CartItem
    {
        // PK

        public int CartItemId
        {
            get; set;
        }

        [Required]

        [Range(1, 1000, ErrorMessage = "Quantity must be at least 1 and realistic.")]

        public int Quantity
        {
            get; set;
        }

        [Required]

        [Range(0.01, 10000, ErrorMessage = "The price must be greater than zero and between 0.01 and 10000.")]

        public decimal Price
        {
            get; set;
        }

        [Required]

        public string CustomerId { get; set; } = string.Empty;

        // FK: Every cart item must refer to a Book (mandatory)

        [Required]

        public int BookId
        {
            get; set;
        }

        // Navigation property: each cart item points to one book (mandatory from the cart side)

        public Book Book { get; set; } = null!;

    }
}

