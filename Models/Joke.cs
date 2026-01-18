using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JokesWebApp.Models
{
    public class Joke
    {
        public int id { get; set; }

        [Required]
        public string JokeQuestion { get; set; }

        [Required]
        public string JokeAnswer { get; set; }

        // Ownership
        [ForeignKey("Creator")]
        public string? CreatorId { get; set; }

        [ValidateNever]
        public IdentityUser? Creator { get; set; }

        //New: TimeStamp
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
