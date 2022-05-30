using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.Data;
using NoMansSkyRecipies.Data.Entities;

namespace NmsRecipes.DAL.Repositories
{
    public class ResourcesRepository : IResourceRepository
    {
        private readonly RecipiesDbContext _dbContext;

        public ResourcesRepository(RecipiesDbContext db)
        {
            this._dbContext = db;
        }

        public int AddItem(RawResource item)
        {
            //this._dbContext.RawResources.Add(item);
            //int res = this._dbContext.SaveChanges();
            //return item.Id;
            return 1;
            //throw new InvalidOperationException("some exception");

        }

        public void DeleteItem(int itemId)
        {
            var itemToDelete = this._dbContext.RawResources.FirstOrDefault(x => x.Id == itemId);
            if (itemToDelete != null)
            {
                this._dbContext.RawResources.Remove(itemToDelete);
                this._dbContext.SaveChanges();
            }
        }

        public IEnumerable<RawResource> GetAllItems()
        {
            return this._dbContext.RawResources
                .Include(x => x.RawResourceType)
                .ToList();
        }

        public IEnumerable<RawResource> GetItemsByCondition(Expression<Func<RawResource, bool>> condition)
        {
            return this._dbContext.RawResources
                .Include(x => x.RawResourceType)
                .Where(condition)
                .ToList();
        }

        public IEnumerable<RawResourceType> GetRawResourceTypes()
        {
            return this._dbContext.RawResourceTypes;
        }

        public IEnumerable<string> GetRawResourcesNames()
        {
            return this._dbContext.RawResources.Select(x => x.Name);
        }

        public RawResource GetItemById(int id)
        {
            return this._dbContext.RawResources
                .Include(x => x.RawResourceType)
                .FirstOrDefault(x => x.Id == id);
        }

        public int UpdateItem(int id, RawResource item)
        {
            int result = 0;
            var targetItem = this._dbContext.RawResources.Find(id);

            if (targetItem != null)
            {
                targetItem.Name = item.Name;
                targetItem.Value = item.Value;
                targetItem.RawResourceTypeId = item.RawResourceTypeId;

                this._dbContext.RawResources.Update(targetItem);
                this._dbContext.SaveChanges();
                result = targetItem.Id;
            }

            return result;
        }

        public bool IsItemExists(string itemName)
        {
            return this._dbContext.RawResources.FirstOrDefault(x => x.Name == itemName) != null;
        }

        public RawResource GetItemByName(string name)
        {
            return this._dbContext.RawResources
                .Include(x => x.RawResourceType)
                .FirstOrDefault(x => x.Name == name);
        }

        public bool IsItemExists(int id)
        {
            var t = this._dbContext.RawResources.FirstOrDefault(x => x.Id == id) != null;
            return t;
        }
    }
}
