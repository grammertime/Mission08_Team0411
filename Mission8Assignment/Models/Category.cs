using System.ComponentModel.DataAnnotations;

namespace Mission08_Team0411.Models // Use your specific namespace
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}