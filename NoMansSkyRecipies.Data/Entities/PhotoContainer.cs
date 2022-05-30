using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NoMansSkyRecipies.Data.Entities
{
    public class ImageContainer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public byte[] ImageBytes { get; set; }
    }
}
