using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace NoMansSkyRecipies.Data.Entities
{
    public  class Recipie
    {
        public Recipie()
        {
        }

        public Recipie(int id, int resultingItemId)
        {
            Id = id;
            ResultingItemId = resultingItemId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ResultingItemId { get; set; }

        public CraftableItem ResultingItem { get; set; }

        public List<NeededResource> NeededResources { get; set; }

        public override string ToString()
        {
            return this.ResultingItem?.Name ?? "";
        }
    }
}
