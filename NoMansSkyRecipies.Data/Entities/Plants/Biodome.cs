using Nms.StaticData.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoMansSkyRecipies.Data.Entities.Plants
{
    public class Biodome 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PlantId { get; set; }

        public Plant Plant { get; set; }

        [NotMapped] 
        public int TotalOutput => this.Plant.SinglePlantAmount * Constants.BiodomeCapacity;
    }
}
