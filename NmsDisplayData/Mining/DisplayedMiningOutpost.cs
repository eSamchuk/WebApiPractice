using NmsDisplayData.Energy;
using System;
using System.Collections.Generic;

namespace NmsDisplayData.Mining
{
    public class DisplayedMiningOutpost
    {
        public bool HaveTeleport { get; set; } = true;
        
        public string MinedResource { get; set; }
        
        public string HotspotClass { get; set; }
        
        public int ExtractorCount { get; set; }

        public int SupplyDepots { get; set; }
        
        public int MaxResourceStored { get; set; }
        
        public int TotalExtractionHourRate { get; set; }
        
        public string TimeToFill => TimeSpan.FromHours((double)MaxResourceStored / TotalExtractionHourRate).ToString(@"d\.h\:mm\:ss");

        public int RequiredPower { get; set; }

        public int EnergyBalance { get; set; }
        
        public DisplayedEnergySupply EnergySupply { get; set; }

        public List<int> ExtractorsOutput { get; set; }
    }
}
