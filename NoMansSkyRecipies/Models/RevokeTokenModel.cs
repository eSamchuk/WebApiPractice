using System.ComponentModel.DataAnnotations;

namespace NoMansSkyRecipies.Models
{
    public class RevokeTokenModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(25)]
        public string UserName { get; set; }

        [Required]
        public string UserRefreshToken { get; set; }
    }
}
