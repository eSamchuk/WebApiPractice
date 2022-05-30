using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NmsRecipes.DAL.Model
{
    public class CraftableItemModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Value { get; set; }
    }
}
