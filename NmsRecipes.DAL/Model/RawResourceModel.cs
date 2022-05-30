using System.ComponentModel.DataAnnotations;
using NmsRecipes.DAL.Resources;

namespace NmsRecipes.DAL.Model
{
    public class RawResourceModel
    {
        [Required(ErrorMessageResourceType = typeof(FormsErrorMessages), ErrorMessageResourceName = nameof(FormsErrorMessages.Name))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(FormsErrorMessages), ErrorMessageResourceName = nameof(FormsErrorMessages.Value))]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(FormsErrorMessages), ErrorMessageResourceName = nameof(FormsErrorMessages.ValueRange))]
        public int Value { get; set; }

        [Required(ErrorMessageResourceType = typeof(FormsErrorMessages), ErrorMessageResourceName = nameof(FormsErrorMessages.ResourceType))]
        public string ResourceType { get; set; }
    }
}
