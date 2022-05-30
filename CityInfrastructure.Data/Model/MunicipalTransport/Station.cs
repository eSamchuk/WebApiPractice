using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CityInfrastructure.Data.Model.MunicipalTransport
{
    public class Station
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Longtitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        public List<Route> PassingRoutes { get; set; }





    }
}