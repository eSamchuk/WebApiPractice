using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using NoMansSkyRecipies.Data.Entities.Energy;

namespace NmsRecipes.DAL.Model
{
    public class OutpostModel
    {
        [Required]
        public string MinedResource { get; set; }

        [Required]
        public string HotspotClass { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SupplyDepots { get; set; }

        [Required]
        public bool HaveTeleport { get; set; } = true;

        [Required]
        [MinLength(1)]
        public List<ExctractorModel> Exctractors { get; set; }

        public EnergySupplyModel EnergySupply { get; set; }
    }
}
