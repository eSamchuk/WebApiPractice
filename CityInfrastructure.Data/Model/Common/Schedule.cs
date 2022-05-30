using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CityInfrastructure.Data.Model.MunicipalTransport;

namespace CityInfrastructure.Data.Model.Common
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        public int RouteId { get; set; }

        public Route Route { get; set; }

        public List<ScheduleRecord> ScheduleRecords { get; set; }

    }
}
