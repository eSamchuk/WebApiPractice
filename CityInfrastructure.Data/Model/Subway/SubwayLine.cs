using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CityInfrastructure.Data.Model
{
    public class SubwayLine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<SubwayStation> Stations { get; set; }



    }
}
