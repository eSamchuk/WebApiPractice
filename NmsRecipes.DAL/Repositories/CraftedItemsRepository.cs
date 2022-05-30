using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NmsDisplayData;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.Data;
using NoMansSkyRecipies.Data.Entities;

namespace NmsRecipes.DAL.Repositories
{
    public class CraftedItemsRepository : ICraftedItemsRepository
    {
        private readonly RecipiesDbContext _dbContext;

        private readonly IRecipeRepository _recipeRepository;

        public CraftedItemsRepository(RecipiesDbContext db, IRecipeRepository recipeRepository)
        {
            this._dbContext = db;
            this._recipeRepository = recipeRepository;
        }

        public int AddItem(CraftableItem item)
        {
            this._dbContext.CraftableItems.Add(item);
            this._dbContext.SaveChanges();
            return item.Id;
        }

        public void DeleteItem(int itemId)
        {
            var itemToDelete = this._dbContext.CraftableItems.Find(itemId);
            if (itemToDelete != null)
            {
                this._dbContext.CraftableItems.Remove(itemToDelete);
                this._dbContext.SaveChanges();
            }
        }

        public IEnumerable<CraftableItem> GetAllItems()
        {
            return this._dbContext.CraftableItems
                .ToList();
        }

        public CraftableItem GetItemById(int id)
        {
            return this._dbContext.CraftableItems
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<CraftableItem> GetItemsByCondition(Expression<Func<CraftableItem, bool>> condition)
        {
            return this._dbContext.CraftableItems
                .Where(condition)
                .ToList();
        }

        public int UpdateItem(int id, CraftableItem item)
        {
            int result = 0;
            var itemToUpdate = this._dbContext.CraftableItems.Find(id);

            if (itemToUpdate != null)
            {
                itemToUpdate.Name = item.Name;
                itemToUpdate.Value = item.Value;
                this._dbContext.CraftableItems.Update(itemToUpdate);
                this._dbContext.SaveChanges();
                result = item.Id;
            }

            return result;
        }

        public bool IsItemExists(string itemName)
        {
            return this._dbContext.CraftableItems.FirstOrDefault(x => x.Name == itemName) != null;
        }

        public CraftableItem GetItemByName(string name)
        {
            return this._dbContext.CraftableItems.FirstOrDefault(x => x.Name == name);
        }

        public bool IsItemExists(int id)
        {
            var t = this._dbContext.CraftableItems.FirstOrDefault(x => x.Id == id) != null;
            return t;
        }

        public DisplayedCraftableItem GetCraftableItemWithFullRecipie(string itemName, int count)
        {
            DisplayedCraftableItem craftableItem = null;
            var item = this.GetItemByName(itemName);

            if (item != null)
            {
                var recipie = this._recipeRepository.GetItemByName(item.Name);

                var neededResources = this.GetItemResourcesByRecipie(recipie);

                craftableItem = new DisplayedCraftableItem()
                {
                    ItemName = item.Name,
                    Value = item.Value,
                    NeededResources = neededResources.OrderBy(x => x.Key).ToDictionary(x => x.Key, z => z.Value.count * count),
                    ResourcesTotalValue = neededResources.Sum(x => x.Value.value)
                };
            }

            return craftableItem;
        }

        /// <summary>
        /// recursively gets resources needed for specified Recipe
        /// </summary>
        /// <param name="recipie">Recipe to get resources for</param>
        /// <returns>Dictionary where key - resource name, value - tuple of needed amount and its value</returns>
        private Dictionary<string, (int count, int value)> GetItemResourcesByRecipie(Recipie recipie)
        {
            Dictionary<string, (int count, int value)> result = new Dictionary<string, (int count, int value)>();

            foreach (var neededResource in recipie.NeededResources)
            {
                if (neededResource.CraftableItemId != null)
                {
                    var subRecipie = this._recipeRepository.GetRecipeWithResourcesByCondition(x =>
                            x.ResultingItemId == neededResource.CraftableItemId);

                    Dictionary<string, (int count, int value)> subresult = GetItemResourcesByRecipie(subRecipie);

                    foreach (var subresultItem in subresult)
                    {
                        int totalNeeded = 1, totalValue = 1;

                        if (neededResource.CraftableItem != null)
                        {
                            totalNeeded = subresultItem.Value.count * neededResource.NeededAmount;
                            totalValue = subresultItem.Value.value * neededResource.NeededAmount;
                        }

                        var alterSubresultItem = new KeyValuePair<string, (int, int)>(subresultItem.Key, (totalNeeded, totalValue));

                        this.AddOrUpdateDictionary(result, alterSubresultItem);
                    }
                }
                else if (neededResource.RawResourceId != null)
                {
                    this.AddOrUpdateDictionary(result, new KeyValuePair<string, (int, int)>(neededResource.RawResource.Name,
                        (neededResource.NeededAmount, neededResource.RawResource.Value * neededResource.NeededAmount)));
                }
            }

            return result;
        }

        /// <summary>
        /// updates Dictionary - adds count and value to existing entries or creates new entries
        /// </summary>
        /// <param name="dictionary">dictionary to update</param>
        /// <param name="item">item to add or update from</param>
        private void AddOrUpdateDictionary(Dictionary<string, (int count, int value)> dictionary, KeyValuePair<string, (int count, int value)> item)
        {
            if (dictionary.ContainsKey(item.Key))
            {
                dictionary[item.Key] = (dictionary[item.Key].count + item.Value.count, dictionary[item.Key].value + item.Value.value);
            }
            else
            {
                dictionary.Add(item.Key, item.Value);
            }
        }
    }
}
