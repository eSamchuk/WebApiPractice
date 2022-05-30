using System.Collections.Generic;

namespace NmsDisplayData.Energy
{
    public class DisplayedEMPlant
    {
        public string HotspotClass { get; set; }

        public int TotalGenerators { get; set; }

        public int TotalOutput { get; set; }

        public List<int> GeneratorsOutput { get; set; }
    }
}
