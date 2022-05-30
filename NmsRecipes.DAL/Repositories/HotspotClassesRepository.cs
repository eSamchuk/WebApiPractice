using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.Data;
using NoMansSkyRecipies.Data.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NmsRecipes.DAL.Repositories
{
    public class HotspotClassesRepository : IHotspotClassesRepository
    {
        private readonly RecipiesDbContext _dbContext;

        public HotspotClassesRepository(RecipiesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsItemExists(int id)
        {
            return this._dbContext.HotspotClasses
                .FirstOrDefault(x => x.Id == id) != null;
        }

        public HotspotClass GetItemById(int id)
        {
            return this._dbContext.HotspotClasses
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<HotspotClass> GetAllItems()
        {
            return this._dbContext.HotspotClasses.ToList();
        }

        public IEnumerable<HotspotClass> GetItemsByCondition(Expression<Func<HotspotClass, bool>> condition)
        {
            return this._dbContext.HotspotClasses.Where(condition);
        }

        public int UpdateItem(int id, HotspotClass item)
        {
            int result = 0;
            var targetItem = this._dbContext.HotspotClasses.Find(id);

            if ( targetItem != null)
            {
                targetItem.Class = item.Class;
                targetItem.MaxConcentration = item.MaxConcentration;
                targetItem.MaxOutput = item.MaxOutput;

                this._dbContext.HotspotClasses.Update(targetItem);
                this._dbContext.SaveChanges();
                result = targetItem.Id;
            }

            return result;
        }

        public int AddItem(HotspotClass item)
        {
            this._dbContext.HotspotClasses.Add(item);
            int res = this._dbContext.SaveChanges();
            return item.Id;
        }

        public void DeleteItem(int itemId)
        {
            var targetItem = this._dbContext.HotspotClasses.Find(itemId);

            if (targetItem != null)
            {
                this._dbContext.HotspotClasses.Remove(targetItem);
                this._dbContext.SaveChanges();
            }
        }
    }
}
