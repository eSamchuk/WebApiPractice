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
    public class ElectroMagneticPlantRepository : IElectroMagneticPlantRepository
    {
        private readonly RecipiesDbContext _dbContext;

        public ElectroMagneticPlantRepository(RecipiesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool IsItemExists(int id)
        {
            return this._dbContext.ElectroMagneticPlants
                .FirstOrDefault(x => x.Id == id) != null;
        }


        public ElectroMagneticPlant GetItemById(int id)
        {
            return this._dbContext.ElectroMagneticPlants
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ElectroMagneticPlant> GetAllItems()
        {
            return this._dbContext.ElectroMagneticPlants;
        }

        public IEnumerable<ElectroMagneticPlant> GetItemsByCondition(Expression<Func<ElectroMagneticPlant, bool>> condition)
        {
            return this._dbContext.ElectroMagneticPlants.Where(condition);
        }

        public int UpdateItem(int id, ElectroMagneticPlant item)
        {
            int result = 0;
            var targetItem = this._dbContext.ElectroMagneticPlants.Find(id);

            if (targetItem != null)
            {
                targetItem.HotspotClassId = item.HotspotClassId;
                targetItem.Generators = item.Generators;

                this._dbContext.ElectroMagneticPlants.Update(targetItem);
                this._dbContext.SaveChanges();
                result = targetItem.Id;
            }

            return result;
        }

        public int AddItem(ElectroMagneticPlant item)
        {
            this._dbContext.ElectroMagneticPlants.Add(item);
            int res = this._dbContext.SaveChanges();
            return item.Id;
        }

        public void DeleteItem(int itemId)
        {
            var targetItem = this._dbContext.ElectroMagneticPlants.Find(itemId);

            if (targetItem != null)
            {
                this._dbContext.ElectroMagneticPlants.Remove(targetItem);
                this._dbContext.SaveChanges();
            }
        }
    }
}
