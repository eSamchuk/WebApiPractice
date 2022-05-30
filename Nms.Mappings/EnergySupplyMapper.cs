using System.Diagnostics.CodeAnalysis;
using NmsDisplayData.Energy;
using NoMansSkyRecipies.Data.Entities.Energy;

namespace Nms.Mappings
{
    public static class EnergySupplyMapper
    {
        public static DisplayedEnergySupply MapToDisplayed([NotNull] this EnergySupply supply)
        {
            return new DisplayedEnergySupply
            {
                BatteriesCount = supply.Batteries,
                SolarPanelsCount = supply.SolarPanels,
                MaxEnergyReserve = supply.MaxEnergyReserve,
                MaxMomentaryOutput = supply.MaxMomentaryOutput,
                IntermediateTimeOutput = supply.IntermediateTimeOutput,
                NightTimeOutput = supply.NightTimeOutput,
                DayTimeOutput = supply.DayTimeOutput
            };
        }
    }
}
