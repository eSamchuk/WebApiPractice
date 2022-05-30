using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CityInfrastructure.Data.Model.Common;

namespace CityInfrastructure.Data.Model.MunicipalTransport
{
    public class Route
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }

        [Required]
        public int RouteTypeId { get; set; }

        public RouteType RouteType { get; set; }

        public List<Station> Stations { get; set; }
    }

}
