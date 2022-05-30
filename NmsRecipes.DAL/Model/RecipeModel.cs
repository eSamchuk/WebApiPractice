using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NmsRecipes.DAL.Model
{
    public class RecipeModel
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int TargetItemId { get; set; }

        [NotNull]
        [MinLength(2, ErrorMessage = "You must provide at least two resources for recipe")]
        [Required(ErrorMessage = "List of needed resources can't be empty")]
        public List<(string resourceName, int neededAmount)> NeededResources { get; set; }
    }
}
