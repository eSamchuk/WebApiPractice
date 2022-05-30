using System.ComponentModel.DataAnnotations;

namespace NoMansSkyRecipies.Models
{
    public class UserModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(25)]
        public string UserName { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(16)]
        public string Password { get; set; }
    }
}
