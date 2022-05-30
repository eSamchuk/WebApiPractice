using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NmsRecipes.DAL.Model
{
    public class ExctractorModel
    {
        [Required]
        [Range(1, 610)]
        public int ExtractionRate { get; set; }
    }
}
