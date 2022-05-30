using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NoMansSkyRecipies.Data.Entities.Energy;

namespace NoMansSkyRecipies.Data.Entities.Resources
{
    public class HotspotClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(1)]
        public string Class { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxOutput { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxConcentration { get; set; }

        public List<ElectroMagneticPlant> ElectroMagneticPlants { get; set; }

        public List<MiningOutpost> MiningOutposts { get; set; }
    }
}