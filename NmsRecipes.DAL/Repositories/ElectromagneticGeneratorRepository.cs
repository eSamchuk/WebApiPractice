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
    public class ElectromagneticGeneratorRepository : IElectroMagneticGeneratorRepository
    {
        private readonly RecipiesDbContext _dbContext;

        public ElectromagneticGeneratorRepository(RecipiesDbContext db)
        {
            this._dbContext = db;
        }

        public bool IsItemExists(int id)
        {
            return this._dbContext.ElectroMagneticPlants
                .FirstOrDefault(x => x.Id == id) != null;
        }


        public ElectromagneticGenerator GetItemById(int id)
        {
            return this._dbContext.ElectromagneticGenerators
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ElectromagneticGenerator> GetAllItems()
        {
            return this._dbContext.ElectromagneticGenerators.ToList();
        }

        public IEnumerable<ElectromagneticGenerator> GetItemsByCondition(
            Expression<Func<ElectromagneticGenerator, bool>> condition)
        {
            return this._dbContext.ElectromagneticGenerators.Where(condition);
        }

        public int UpdateItem(int id, ElectromagneticGenerator item)
        {
            int result = 0;
            var targetItem = this._dbContext.ElectromagneticGenerators.Find(id);

            if (targetItem != null)
            {
                targetItem.PlantId = item.PlantId;
                targetItem.Output = item.Output;

                this._dbContext.ElectromagneticGenerators.Update(targetItem);
                this._dbContext.SaveChanges();
                result = targetItem.Id;
            }

            return result;
        }

        public int AddItem(ElectromagneticGenerator item)
        {
            this._dbContext.ElectromagneticGenerators.Add(item);
            int res = this._dbContext.SaveChanges();
            return item.Id;
        }

        public void DeleteItem(int itemId)
        {
            var targetItem = this._dbContext.ElectromagneticGenerators.Find(itemId);

            if (targetItem != null)
            {
                this._dbContext.ElectromagneticGenerators.Remove(targetItem);
                this._dbContext.SaveChanges();
            }
        }
    }
}
