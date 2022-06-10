using NmsDisplayData;
using NoMansSkyRecipies.Data.Entities;

namespace Nms.Mappings
{
    public static class ResourceMapper
    {
        public static DisplayedResource MapToDisplayedResource(this RawResource resource)
        {
            DisplayedResource result = null;

            if (resource != null)
            {
                result = new DisplayedResource()
                {
                    Id = resource.Id,
                    ResourceName = resource.Name,
                    ResourceTypeName = resource.RawResourceType?.ResourceTypeName,
                    Value = resource.Value
                };
            }

            return result;
        }
    }
}
