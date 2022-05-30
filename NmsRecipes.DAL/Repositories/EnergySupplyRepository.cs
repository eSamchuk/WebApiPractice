using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NmsRecipes.DAL.Interfaces;
using NoMansSkyRecipies.Data;
using NoMansSkyRecipies.Data.Entities.Energy;

namespace NmsRecipes.DAL.Repositories
{
    public class EnergySupplyRepository : IEnergySupplyRepository
    {
        private readonly RecipiesDbContext _dbContext;

        public EnergySupplyRepository(RecipiesDbContext db)
        {
            this._dbContext = db;
        }

        public bool IsItemExists(int id)
        {
            return this._dbContext.EnergySupplies
                .FirstOrDefault(x => x.Id == id) != null;
        }

        public EnergySupply GetItemById(int id)
        {
            return this._dbContext.EnergySupplies
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<EnergySupply> GetAllItems()
        {
            return this._dbContext.EnergySupplies.ToList();
        }

        public IEnumerable<EnergySupply> GetItemsByCondition(Expression<Func<EnergySupply, bool>> condition)
        {
            return this._dbContext.EnergySupplies.Where(condition);
        }

        public int UpdateItem(int id, EnergySupply item)
        {
            int result = 0;
            var targetItem = this._dbContext.EnergySupplies.Find(id);

            if (targetItem != null)
            {
                targetItem.PlantId = item.PlantId;
                targetItem.Batteries = item.Batteries;
                targetItem.SolarPanels = item.SolarPanels;

                this._dbContext.EnergySupplies.Update(targetItem);
                this._dbContext.SaveChanges();
                result = targetItem.Id;
            }

            return result;
        }

        public int AddItem(EnergySupply item)
        {
            this._dbContext.EnergySupplies.Add(item);
            int res = this._dbContext.SaveChanges();
            return item.Id;
        }

        public void DeleteItem(int itemId)
        {
            var targetItem = this._dbContext.EnergySupplies.Find(itemId);

            if (targetItem != null)
            {
                this._dbContext.EnergySupplies.Remove(targetItem);
                this._dbContext.SaveChanges();
            }
        }
    }
}
