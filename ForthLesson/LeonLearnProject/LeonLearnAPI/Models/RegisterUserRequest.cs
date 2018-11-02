using System.ComponentModel.DataAnnotations;

namespace LeonLearnAPI.Models
{
    public class RegisterUserRequest
    {
        [Required]  
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
    }
}