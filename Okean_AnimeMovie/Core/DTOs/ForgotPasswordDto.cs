using System.ComponentModel.DataAnnotations;

namespace Okean_AnimeMovie.Core.DTOs
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}


