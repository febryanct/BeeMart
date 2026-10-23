using System.ComponentModel.DataAnnotations;

namespace BeeMart.backend.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string RePassword { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
    }

    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
    }
}