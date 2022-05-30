using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using NoMansSkyRecipies.Data.Entities.Resources;

namespace NoMansSkyRecipies.Data.Entities.Energy
{
    public class ElectroMagneticPlant
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int HotspotClassId { get; set; }

        public HotspotClass HotspotClass { get; set; }

        public List<ElectromagneticGenerator> Generators { get; set; }

        [NotMapped] 
        public int TotalOutput => this.Generators.Sum(x => x.Output);
    }
}
