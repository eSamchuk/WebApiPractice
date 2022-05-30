using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;

namespace NoMansSkyRecipies.Data.Entities
{
    public class CraftableItem
    {
        public CraftableItem()
        {
        }

        public CraftableItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Value { get; set; }

        public List<Recipie> Recipies { get; set; }

    }
}
