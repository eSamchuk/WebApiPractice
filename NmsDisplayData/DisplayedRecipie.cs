using System;
using System.Collections.Generic;
using System.Text;

namespace NmsDisplayData
{
    public class DisplayedRecipe : ILinkedResource
    {
        public int Id { get; set; }

        public string ItemName { get; set; }

        public List<DisplayedNeededResource> NeededResources { get; set; }

        public List<ResourceUri> Links { get; set; }
    }
}
