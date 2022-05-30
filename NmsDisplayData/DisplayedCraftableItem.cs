using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NmsDisplayData
{
    public class DisplayedCraftableItem : IUsable, ILinkedResource
    {
        [JsonProperty(Order = 1)]
        public int Id { get; set; }
        public List<ResourceUri> Links { get; set; }

        public string ItemName { get; set; }

        public int? TotalIngridients => this.NeededResources?.Count;

        public Dictionary<string, int> NeededResources { get; set; }

        public int Value { get; set; }

        public int? ResourcesTotalValue { get; set; }

        public double? ProfitRatio => this.ResourcesTotalValue.HasValue ? Math.Round((double)(this.Value) / this.ResourcesTotalValue.Value, 2, MidpointRounding.AwayFromZero) : new double?();

        public List<string> UsedIn { get; set; }

    }
}
