using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DotNetBookstore.Models
{
    public class Order
    {
        // PK

        public int OrderId
        {
            get; set;
        }

        [Display(Name = "Order Date")]

        public DateTime OrderDate
        {
            get; set;
        }

        [Display(Name = "Order Total")]

        public decimal OrderTotal
        {
            get; set;
        }

        [Required]

        [Display(Name = "First Name")]

        [MaxLength(50)]

        public string FirstName { get; set; } = string.Empty;

        [Required]

        [Display(Name = "Last Name")]

        [MaxLength(50)]

        public string LastName { get; set; } = string.Empty;

        [Required]

        [MaxLength(150)]

        public string Address { get; set; } = string.Empty;

        [Required]

        [MaxLength(50)]

        public string City { get; set; } = string.Empty;

        [Required]

        [MaxLength(2)]

        public string Province { get; set; } = string.Empty;

        [Required]

        [MaxLength(10)]

        [Display(Name = "Postal Code")]

        public string PostalCode { get; set; } = string.Empty;

        [Required]

        [MaxLength(15)]

        public string Phone { get; set; } = string.Empty;

        [Required]

        [Display(Name = "Email")]

        [MaxLength(100)]

        public string CustomerId { get; set; } = string.Empty;

        // Navigation property: Order can have many details (optional from the order side)
        [ValidateNever]
        public ICollection<OrderDetail> OrderDetails { get; set; } = [];
    }
}
