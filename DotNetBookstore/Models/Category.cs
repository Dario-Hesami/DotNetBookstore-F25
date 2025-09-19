namespace DotNetBookstore.Models
{
    public class Category
    {
        // pk field should be always be called either Id or <ModelName>Id
        public int CategoryId { get; set;
        }
        public string Name { get; set;
        } = string.Empty;
    }
}
