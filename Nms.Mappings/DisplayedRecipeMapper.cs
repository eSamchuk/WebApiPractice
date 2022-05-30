using System.Linq;
using NmsDisplayData;
using NoMansSkyRecipies.Data.Entities;

namespace Nms.Mappings
{
    public static class DisplayedRecipeMapper
    {
        public static DisplayedRecipe MapToDisplayedRecipie(this Recipie recipe)
        {
            var drList = recipe.NeededResources.Select(neededResource => new DisplayedNeededResource()
            {
                ResourceName = neededResource.RawResource?.Name ?? neededResource.CraftableItem?.Name,
                ResourceTypeName = neededResource.RawResource?.RawResourceType?.ResourceTypeName ?? null,
                Value = (neededResource.RawResource?.Value ?? neededResource.CraftableItem?.Value) * neededResource.NeededAmount ?? 0,
                NeededAmount = neededResource.NeededAmount
            }).ToList();

            return new DisplayedRecipe()
            {
                Id = recipe.Id,
                ItemName = recipe.ResultingItem.Name,
                NeededResources = drList.Any() ? drList : null
            };
        }
    }
}
