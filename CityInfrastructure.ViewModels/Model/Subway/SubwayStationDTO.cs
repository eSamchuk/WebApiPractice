using System;

namespace CityInfrastructure.DTO.Model.Subway
{
    public class SubwayStationDTO
    {
        public string Name { get; set; }

        public string Line { get; set; }

        public double Depth { get; set; }

        public string CurrentStatus { get; set; }

        public DateTime ConstructionDate { get; set; }

        public string IntersectingStation { get; set; }
    }
}
