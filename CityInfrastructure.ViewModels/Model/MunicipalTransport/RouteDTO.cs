using System.Collections.Generic;
using CityInfrastructure.DTO.Model.Common;

namespace CityInfrastructure.DTO.Model.MunicipalTransport
{
    public class RouteDTO
    {
        public string RouteFullName { get; set; }

        public List<StationDTO> Stations { get; set; }

        public List<ScheduleRecordDTO> ScheduleRecords { get; set; }
    }

}
