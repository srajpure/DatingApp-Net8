using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class LoginDto
    {
        [Required]
        [MaxLength(100)]
        public required string UserName { get; set; }

        public required string Password { get; set; }
    }
}
