using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NmsRecipes.DAL.Model
{
    public class EnergySupplyModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int SolarPanels { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Batteries { get; set; }

        public ElectroMagneticPlantModel Plant { get; set; }
    }
}
