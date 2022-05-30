using System;
using System.Collections.Generic;
using System.Text;

namespace NmsRecipes.DAL.Interfaces
{
    public interface INamedRepository<T> : IRepository<T> where T : class
    {
        bool IsItemExists(string itemName);
        T GetItemByName(string name);
    }
}
