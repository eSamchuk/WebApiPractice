using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfrastructure.Data.Model
{
    public class SubwayStation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required] 
        public int LineId { get; set; }

        public SubwayLine Line { get; set; }

        [Required]
        public double Depth { get; set; }

        [Required]
        public DateTime ConstructionDate { get; set; }

        public List<SubwayStationStatus> Statuses { get; set; }

        public SubwayStation IntersectingStation { get; set; }
    }
}
