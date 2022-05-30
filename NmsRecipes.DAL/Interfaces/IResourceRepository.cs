using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NoMansSkyRecipies.Data.Entities;

namespace NmsRecipes.DAL.Interfaces
{
    public interface IResourceRepository : INamedRepository<RawResource>
    {
        IEnumerable<RawResourceType> GetRawResourceTypes();
        IEnumerable<string> GetRawResourcesNames();

    }
}
