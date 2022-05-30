using System;
using System.Collections.Generic;
using System.Text;

namespace NmsRecipes.DAL.Model
{
    public class ElectroMagneticPlantModel
    {
        public string HotspotClass { get; set; }

        public List<ElectromagneticGeneratorModel> Generators { get; set; }
    }
}
