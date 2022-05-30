using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.Data;
using NoMansSkyRecipies.Data.Entities.Resources;

namespace NmsRecipes.DAL.Repositories
{
    public class ExctractorRepository : IExctractorRepository
    {
        private readonly RecipiesDbContext _dbContext;

        public ExctractorRepository(RecipiesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsItemExists(int id)
        {
            return this._dbContext.Extractors.FirstOrDefault(x => x.Id == id) != null;
        }

        public Extractor GetItemById(int id)
        {
            return this._dbContext.Extractors.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Extractor> GetAllItems()
        {
            return this._dbContext.Extractors.ToList();
        }

        public IEnumerable<Extractor> GetItemsByCondition(Expression<Func<Extractor, bool>> condition)
        {
            return this._dbContext.Extractors.Where(condition).ToList();
        }

        public int UpdateItem(int id, Extractor item)
        {
            int result = 0;
            var targetItem = this._dbContext.Extractors.Find(id);

            if (targetItem != null)
            {
                //TODO
                //targetItem.ResourceTypeId = item.ResourceTypeId;
                //targetItem.HotspotClassId = item.HotspotClassId;
                //targetItem.EnergySupply = item.EnergySupply;
                //targetItem.SupplyDepots = item.SupplyDepots;
                //targetItem.HaveTeleport = item.HaveTeleport;

                this._dbContext.Extractors.Update(targetItem);
                this._dbContext.SaveChanges();
                result = targetItem.Id;
            }

            return result;
        }

        public int AddItem(Extractor item)
        {
            this._dbContext.Extractors.Add(item);
            this._dbContext.SaveChanges();

            return item.Id;
        }

        public void DeleteItem(int itemId)
        {
            var targetItem = this._dbContext.Extractors.Find(itemId);

            if (targetItem != null)
            {
                this._dbContext.Extractors.Remove(targetItem);
                this._dbContext.SaveChanges();
            }
        }
    }
}
