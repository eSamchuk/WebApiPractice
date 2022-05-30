using System.Text.Json.Serialization;

namespace NmsDisplayData
{
    public class DisplayedNeededResource : DisplayedResource
    {
        public int NeededAmount { get; set; }
    }
}
