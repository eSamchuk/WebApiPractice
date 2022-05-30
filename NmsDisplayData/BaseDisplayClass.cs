using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NmsDisplayData
{
    public interface ILinkedResource
    {
        [JsonProperty(Order = int.MaxValue)]
        public List<ResourceUri> Links { get; set; }

    }
}
