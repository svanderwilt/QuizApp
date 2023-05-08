
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {

        public string Username { get; set; }

        [Required] public string KnownAs { get; set; }

        [Required] public DateOnly? DateOfBirth { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 6)]
        public string Password { get; set; }
    }
}