using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.Data;
using NoMansSkyRecipies.Data.Entities.Resources;

namespace NmsRecipes.DAL.Repositories
{
    public class MiningOutpostRepository : IMiningOutpostRepository
    {
        private readonly RecipiesDbContext _dbContext;

        public MiningOutpostRepository(RecipiesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsItemExists(int id)
        {
            return this._dbContext.MiningOutposts
              .FirstOrDefault(x => x.Id == id) != null;
        }

        public MiningOutpost GetItemById(int id)
        {
            return this._dbContext.MiningOutposts
                .Include(x => x.Extractors)
                .Include(x => x.EnergySupply)
                .Include(x => x.ResourceType)
                .Include(x => x.HotspotClass)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<MiningOutpost> GetAllItems()
        {
            return this._dbContext.MiningOutposts
                .Include(x => x.Extractors)
                .Include(x => x.EnergySupply)
                .Include(x => x.ResourceType)
                .Include(x => x.HotspotClass)
                .ToList();
        }

        public IEnumerable<MiningOutpost> GetItemsByCondition(Expression<Func<MiningOutpost, bool>> condition)
        {
            return this._dbContext.MiningOutposts.Where(condition);
        }

        public int UpdateItem(int id, MiningOutpost item)
        {
            int result = 0;
            var targetItem = this._dbContext.MiningOutposts.Find(id);

            if (targetItem != null)
            {
                //TODO
                //targetItem.ResourceTypeId = item.ResourceTypeId;
                //targetItem.HotspotClassId = item.HotspotClassId;
                //targetItem.EnergySupply = item.EnergySupply;
                //targetItem.SupplyDepots = item.SupplyDepots;
                //targetItem.HaveTeleport = item.HaveTeleport;


                this._dbContext.MiningOutposts.Update(targetItem);
                this._dbContext.SaveChanges();
                result = targetItem.Id;
            }

            return result;
        }

        public int AddItem(MiningOutpost item)
        {
            this._dbContext.MiningOutposts.Add(item);
            int res = this._dbContext.SaveChanges();
            return item.Id;
        }

        public void DeleteItem(int itemId)
        {
            var targetItem = this._dbContext.MiningOutposts.Find(itemId);

            if (targetItem != null)
            {
                this._dbContext.MiningOutposts.Remove(targetItem);
                this._dbContext.SaveChanges();
            }
        }

      
    }
}
