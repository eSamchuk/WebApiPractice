using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NmsDisplayData;

namespace NoMansSkyRecipies.Controllers
{
    [Area("api")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public abstract List<ResourceUri> GetResourceUris(int id, ApiVersion apiVersion);
    }
}
