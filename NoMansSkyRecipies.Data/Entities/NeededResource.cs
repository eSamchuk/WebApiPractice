using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace NoMansSkyRecipies.Data.Entities
{
    public class NeededResource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int RecipieId { get; set; }
        public Recipie Recipie { get; set; }

        public int? RawResourceId { get; set; }

        public RawResource RawResource { get; set; }
        
        public int? CraftableItemId { get; set; }

        public CraftableItem CraftableItem { get; set; }

        [Required]
        public int NeededAmount { get; set; }

        public override string ToString()
        {
            return $"{this.RawResource?.Name ?? this.CraftableItem?.Name ?? "N/a"} {NeededAmount}";
        }
    }
}
