using System.Collections.Generic;

namespace NmsDisplayData
{
    internal interface IUsable
    {
        List<string> UsedIn { get; set; }
    }
}
