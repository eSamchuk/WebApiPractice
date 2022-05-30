using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoMansSkyRecipies.Data.Entities.Resources
{
    public class Extractor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ExtractionRate { get; set; }

        [Required]
        public int MiningOutpostId { get; set; }

        public MiningOutpost MiningOutpost { get; set; }

    }
}
