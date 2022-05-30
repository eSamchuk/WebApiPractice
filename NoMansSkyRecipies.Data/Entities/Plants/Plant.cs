using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoMansSkyRecipies.Data.Entities.Plants
{
    public class Plant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int SinglePlantAmount { get; set; }

        public int ResourceId { get; set; }

        public RawResource Resource { get; set; }

        public string Climate { get; set; }

        public TimeSpan GrowTime { get; set; }

    }
}
