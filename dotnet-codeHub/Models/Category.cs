using System.ComponentModel.DataAnnotations;

namespace dotnet_codeHub.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsVisible { get; set; }

    }
}
