using System.ComponentModel.DataAnnotations;

namespace NoMansSkyRecipies.Models
{
    public class RefreshTokenModel
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
