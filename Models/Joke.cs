using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JokesWebApp.Models
{
    public class Joke
    {
        [Key]
        public int id { get; set; }

        [Required, StringLength(200)]
        public string JokeQuestion { get; set; } = string.Empty;

        [Required, StringLength(500)]
        public string JokeAnswer { get; set; } = string.Empty;

        // Ownership
        // [Required]
        [BindNever]
        public string? CreatorId { get; set; } = default!;

        [ValidateNever]
        public IdentityUser? Creator { get; set; } = default!;

        //New: TimeStamp
        public DateTime CreatedAt { get; set; }
    }
}
