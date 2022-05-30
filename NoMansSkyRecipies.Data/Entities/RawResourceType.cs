using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace NoMansSkyRecipies.Data.Entities
{
    public class RawResourceType
    {
        public RawResourceType()
        {
            
        }

        public RawResourceType(int id, string resourceTypeName)
        {
            Id = id;
            ResourceTypeName = resourceTypeName;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ResourceTypeName { get; set; }

        public List<RawResource> RawResources { get; set; }

    }
}
