using System.ComponentModel.DataAnnotations;

namespace DotNetBookstore.Models
{
    public class Category
    {
        // pk field should be always be called either Id or <ModelName>Id
        public int CategoryId
        {
            get; set;
        }

        [Required(ErrorMessage = "A customized error message")]

        [MaxLength(100)]

        public string Name { get; set; } = string.Empty;

        // Navigation property: A category can have many books (optional from the category side)

        public ICollection<Book> Books { get; set; } = [];
    }
}
