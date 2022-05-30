namespace NmsDisplayData.Energy
{
    public class DisplayedEnergySupply
    {
        public int SolarPanelsCount { get; set; }

        public int BatteriesCount { get; set; }

        public int MaxMomentaryOutput { get; set; }

        public int MaxEnergyReserve { get; set; }

        public int DayTimeOutput { get; set; }

        public int IntermediateTimeOutput { get; set; }

        public int NightTimeOutput { get; set; }

    }
}
