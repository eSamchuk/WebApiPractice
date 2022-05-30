using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;

namespace NoMansSkyRecipies.Data.Entities
{
    public class RawResource
    {
        public RawResource()
        {
            
        }

        public RawResource(int id, string name, int rawResourceTypeId)
        {
            this.Id = id;
            this.Name = name;
            this.RawResourceTypeId = rawResourceTypeId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Value{ get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int RawResourceTypeId { get; set; }
        public RawResourceType RawResourceType { get; set; }
    }
}
