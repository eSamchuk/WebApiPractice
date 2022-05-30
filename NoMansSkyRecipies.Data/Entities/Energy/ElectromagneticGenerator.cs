using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoMansSkyRecipies.Data.Entities.Energy
{
    public class ElectromagneticGenerator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Output { get; set; }

        [Required]
        public int PlantId { get; set; }

        public ElectroMagneticPlant Plant { get; set; }
    }
}
