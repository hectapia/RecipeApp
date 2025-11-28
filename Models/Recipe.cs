using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(4000)]
        public string Instructions { get; set; } = string.Empty;

        // Foreign key to User
        public int? OwnerUserId { get; set; }   // nullable so you can have public recipes

        // Optional navigation property
        public User? Owner { get; set; }
    }
}
