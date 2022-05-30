using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CityInfrastructure.Data.Model
{
    public class SubwayStationStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string StatusName { get; set; }

        public int SubwayStationId { get; set; }
        public SubwayStation SubwayStation { get; set; }
    }
}
