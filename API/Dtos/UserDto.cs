using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class UserDto
    {
        [Required]
        [MaxLength(100)]
        public required string UserName { get; set; }

        public required string Token { get; set; }
    }
}
