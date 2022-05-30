using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CityInfrastructure.DTO.Model.Subway
{
    public class SubwayLineDTO
    {
        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<string> StationsNames { get; set; }

        public int StationsCount => this.StationsNames.Count;
    }
}
