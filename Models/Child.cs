using System.ComponentModel.DataAnnotations;
using ChildrenWebApp.Validation;

namespace ChildrenWebApp.Models
{
    public class Child
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[A-Z][a-zA-Z]*\s[A-Z][a-zA-Z]*$", ErrorMessage = "Name should contain at least 2 words as First and last name and each word should start with a capital letter.")]
        public string Name { get; set; }

        public string Gender { get; set; }

        [Required]
        public List<string> FavoriteAnimals { get; set; }

        // This property is to display FavoriteAnimals as a string
        public string FavoriteAnimalsDisplay => FavoriteAnimals != null && FavoriteAnimals.Any()
        ? string.Join(", ", FavoriteAnimals)
        : "None";

        [CustomEmail]
        public string Email { get; set; }

        public bool IsExported { get; set; } // To track export status

        public Child()
        {
            
        }
    }
}
