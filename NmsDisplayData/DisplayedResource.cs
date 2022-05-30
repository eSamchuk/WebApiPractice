using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Nms.StaticData.LocalizationResourcesKeys;
using NmsDisplayData.Resources;

namespace NmsDisplayData
{
    public class DisplayedResource : IUsable, ILinkedResource
    {
        [JsonProperty(Order = 1)]
        public int Id { get; set; }

        public List<ResourceUri> Links { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = RawResourceLocalizationKeys.ResourceName)]
        [JsonPropertyName("Name")]
        public string ResourceName { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = RawResourceLocalizationKeys.ResourceType)]
        public string ResourceTypeName { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = RawResourceLocalizationKeys.ResourceValue)]
        public int Value { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = RawResourceLocalizationKeys.ResourceUsedIn)]
        public List<string> UsedIn { get; set; }
    }
}
