using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NmsDisplayData;
using NoMansSkyRecipies.Data.Entities;

namespace NmsRecipes.DAL.Interfaces
{
    public interface ICraftedItemsRepository : INamedRepository<CraftableItem>
    {
        DisplayedCraftableItem GetCraftableItemWithFullRecipie(string name, int count);
    }
}
