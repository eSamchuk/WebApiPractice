using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CityInfrastructure.Data.Model.MunicipalTransport
{
    public class RouteType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Route> Routes { get; set; }
    }
}
