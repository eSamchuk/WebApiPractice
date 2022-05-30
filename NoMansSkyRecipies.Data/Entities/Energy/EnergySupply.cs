using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Nms.StaticData.Constants;

namespace NoMansSkyRecipies.Data.Entities.Energy
{
    public class EnergySupply
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SolarPanels { get; set; }

        public int Batteries{ get; set; }

        [AllowNull]
        public int? PlantId { get; set; }

        public ElectroMagneticPlant Plant { get; set; }

        [NotMapped]
        public int MaxMomentaryOutput => this.SolarPanels * Constants.SolarPanelDayOutput + (this.Plant?.TotalOutput ?? 0);

        [NotMapped] 
        public int MaxEnergyReserve => this.Batteries * Constants.BatteryMaxCapacity;

        [NotMapped]
        public int DayTimeOutput => this.SolarPanels * Constants.SolarPanelDayOutput + (this.Plant?.TotalOutput ?? 0);

        [NotMapped]
        public int IntermediateTimeOutput => this.Batteries * Constants.SolarPanelIntermediateOutput + (this.Plant?.TotalOutput ?? 0);

        [NotMapped]
        public int NightTimeOutput => (this.Plant?.TotalOutput ?? 0);

    }
}