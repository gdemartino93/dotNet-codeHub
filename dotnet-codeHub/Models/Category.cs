using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_codeHub.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [MinLength(1,ErrorMessage = "Il nome non può essere vuoto")]
        [MaxLength(15,ErrorMessage = "Il nome non può superare 15 caratteri")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [DisplayName("Ordine visualizzazione")]
        [Range(1,15,ErrorMessage = "Deve essere un numero da 1 a 15")]
        public int DisplayOrder { get; set; }
        public bool IsVisible { get; set; }

    }
}
