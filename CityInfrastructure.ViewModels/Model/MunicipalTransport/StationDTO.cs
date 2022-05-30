using System.Collections.Generic;

namespace CityInfrastructure.DTO.Model.MunicipalTransport
{
    public class StationDTO
    {
        public string Name { get; set; }

        public double Longtitude { get; set; }

        public double Latitude { get; set; }

        public List<string> PassingRoutes { get; set; }
    }
}