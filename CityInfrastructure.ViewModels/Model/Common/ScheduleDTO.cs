using CityInfrastructure.DTO.Model.MunicipalTransport;
using System.Collections.Generic;

namespace CityInfrastructure.DTO.Model.Common
{
    public class ScheduleDTO
    {
        public string RouteName{ get; set; }

        public List<ScheduleRecordDTO> ScheduleRecords { get; set; }

    }
}
