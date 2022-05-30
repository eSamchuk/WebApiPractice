using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NoMansSkyRecipies.Data.Entities;

namespace NmsRecipes.DAL.Interfaces
{
    public interface IRecipeRepository : INamedRepository<Recipie>
    {
        IEnumerable<Recipie> GetAllrecipesWithoutResources();
        IEnumerable<Recipie> GetAllrecipesWithoutResourcesByCondition(Expression<Func<Recipie, bool>> expression);
        Recipie GetRecipeWithResourcesByCondition(Expression<Func<Recipie, bool>> expression);

    }
}
