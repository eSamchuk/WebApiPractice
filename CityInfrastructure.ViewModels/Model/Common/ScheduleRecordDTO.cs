using System;

namespace CityInfrastructure.DTO.Model.Common
{
    public class ScheduleRecordDTO
    {
        public int ScheduleId { get; set; }

        //public Schedule Schedule { get; set; }

        public DateTime ArriveTime { get; set; }

        public int StationId { get; set; }
        //public Station Station { get; set; }

        public int RouteId { get; set; }
        //public Route Route { get; set; }
    }
}