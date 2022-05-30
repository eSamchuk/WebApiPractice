using Microsoft.EntityFrameworkCore;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.Data;
using NoMansSkyRecipies.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NmsRecipes.DAL.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private RecipiesDbContext dbContext;

        public RecipeRepository(RecipiesDbContext db)
        {
            this.dbContext = db;
        }

        public int AddItem(Recipie item)
        {
            this.dbContext.Recipies.Add(item);
            int res = this.dbContext.SaveChanges();
            return item.Id;
        }

        public void DeleteItem(int itemId)
        {
            var itemToDelete = this.dbContext.Recipies
                .Include(x => x.NeededResources)
                .FirstOrDefault(x => x.Id == itemId);

            if (itemToDelete != null)
            {
                foreach (var neededResource in itemToDelete.NeededResources)
                {
                    this.dbContext.NeededResources.Remove(neededResource);
                }
                
                this.dbContext.Recipies.Remove(itemToDelete);
                this.dbContext.SaveChanges();
            }
        }

        public IEnumerable<Recipie> GetAllItems()
        {
            return this.dbContext.Recipies
                .Include(x => x.NeededResources).ThenInclude(x => x.CraftableItem)
                .Include(x => x.NeededResources).ThenInclude(x => x.RawResource).ThenInclude(x => x.RawResourceType)
                .Include(x => x.ResultingItem)
                .ToList();
        }

        public Recipie GetItemById(int id)
        {
            return this.dbContext.Recipies
                .Include(x => x.NeededResources).ThenInclude(x => x.CraftableItem)
                .Include(x => x.NeededResources).ThenInclude(x => x.RawResource).ThenInclude(x => x.RawResourceType)
                .Include(x => x.ResultingItem)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Recipie> GetItemsByCondition(Expression<Func<Recipie, bool>> condition)
        {
            return this.dbContext.Recipies
                .Include(x => x.NeededResources).ThenInclude(x => x.CraftableItem)
                .Include(x => x.NeededResources).ThenInclude(x => x.RawResource).ThenInclude(x => x.RawResourceType)
                .Include(x => x.ResultingItem)
                .Where(condition)
                .ToList();
        }

        public int UpdateItem(int id, Recipie item)
        {
            int result = 0;
            var targetItem = this.dbContext.Recipies.
                    Where(x => x.Id == id)
                .Include(x => x.NeededResources).ThenInclude(x => x.CraftableItem)
                .Include(x => x.NeededResources).ThenInclude(x => x.RawResource)
                .FirstOrDefault();

            if (targetItem != null)
            {
                item.Id = targetItem.Id;

                foreach (var resource in targetItem.NeededResources)
                {
                    this.dbContext.Entry(resource).State = EntityState.Deleted;
                }

                targetItem.NeededResources = item.NeededResources;

                this.dbContext.Recipies.Update(targetItem);
                this.dbContext.SaveChanges();
                result = targetItem.Id;
            }

            return result;
        }

        public IEnumerable<Recipie> GetAllrecipesWithoutResources()
        {
            return this.dbContext.Recipies
                .Include(x => x.ResultingItem)
                .ToList();
        }

        public IEnumerable<Recipie> GetAllrecipesWithoutResourcesByCondition(Expression<Func<Recipie, bool>> expression)
        {
            return this.dbContext.Recipies
                .Include(x => x.ResultingItem)
                .Where(expression)
                .ToList();
        }

        public bool IsItemExists(string itemName)
        {
            return this.dbContext.Recipies
                .Include(x => x.ResultingItem)
                .FirstOrDefault(x => x.ResultingItem.Name == itemName) != null;
        }

        public Recipie GetItemByName(string name)
        {
            return this.dbContext.Recipies.Include(x => x.ResultingItem)
                .Include(x => x.NeededResources).ThenInclude(x => x.RawResource).ThenInclude(x => x.RawResourceType)
                .Include(z => z.NeededResources).ThenInclude(b => b.CraftableItem)
                .FirstOrDefault(x => x.ResultingItem.Name == name);
        }

        public Recipie GetRecipeWithResourcesByCondition(Expression<Func<Recipie, bool>> expression)
        {
            return this.dbContext.Recipies.Include(x => x.ResultingItem)
                .Include(z => z.NeededResources).ThenInclude(c => c.RawResource)
                .Include(z => z.NeededResources).ThenInclude(b => b.CraftableItem)
                .FirstOrDefault(expression);
        }

        public bool IsItemExists(int id)
        {
            return this.dbContext.CraftableItems.FirstOrDefault(x => x.Id == id) != null;
        }
    }
}
