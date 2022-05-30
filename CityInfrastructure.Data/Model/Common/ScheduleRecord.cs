using System;
using System.ComponentModel.DataAnnotations;
using CityInfrastructure.Data.Model.MunicipalTransport;

namespace CityInfrastructure.Data.Model.Common
{
    public class ScheduleRecord
    {
        [Key]
        public int Id { get; set; }

        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }

        public DateTime ArriveTime { get; set; }

        public int StationId { get; set; }
        public Station Station { get; set; }

        public int RouteId { get; set; }
        public Route Route { get; set; }
    }
}