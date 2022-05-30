using System.Linq;
using NmsDisplayData.Mining;
using NoMansSkyRecipies.Data.Entities.Resources;

namespace Nms.Mappings
{
    public static class MiningOutpostMapper
    {
        public static DisplayedMiningOutpost MapToDisplayed(this MiningOutpost outpost)
        {
            return new DisplayedMiningOutpost
            {
                MinedResource = outpost.ResourceType.Name,
                HotspotClass = outpost.HotspotClass.Class,
                ExtractorCount = outpost.ExtractorCount,
                HaveTeleport = outpost.HaveTeleport,
                MaxResourceStored = outpost.MaxResourceStored,
                SupplyDepots = outpost.SupplyDepots,
                RequiredPower = outpost.RequiredPower,
                TotalExtractionHourRate = outpost.TotalExtractionHourRate, 
                ExtractorsOutput = outpost.Extractors.Select(x => x.ExtractionRate).OrderByDescending(x => x).ToList(),
                EnergyBalance = outpost.EnergyBalance,
                EnergySupply = outpost.EnergySupply.MapToDisplayed()
            };
        }
    }
}
