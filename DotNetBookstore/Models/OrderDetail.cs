using System.ComponentModel.DataAnnotations;

namespace DotNetBookstore.Models
{
    public class OrderDetail
    {
        // PK

        public int OrderDetailId
        {
            get; set;
        }

        [Required]

        public int Quantity
        {
            get; set;
        }

        [Required]

        public decimal Price
        {
            get; set;
        }

        // FK: must refer to an order (mandatory)

        [Required]

        public int OrderId
        {
            get; set;
        }

        // Navigation property: each detail is for one order (mandatory from the detail side)

        public Order Order { get; set; } = null!;

        // FK: must refer to a book (mandatory)

        [Required]

        public int BookId
        {
            get; set;
        }

        // Navigation property: each detail is for one book (mandatory from the detail side)

        public Book Book { get; set; } = null!;
    }
}
