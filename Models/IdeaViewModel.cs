using System.ComponentModel.DataAnnotations;
namespace GoldDuck.Models
{
    public class IdeaViewModel : BaseEntity
    {
        [Required]
        [MinLength(10)]
        public string body {get; set;}

    }
}