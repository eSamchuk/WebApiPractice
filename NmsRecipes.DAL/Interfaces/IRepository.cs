using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NmsRecipes.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {

        bool IsItemExists(int id);
        T GetItemById(int id);

        IEnumerable<T> GetAllItems();

        IEnumerable<T> GetItemsByCondition(Expression<Func<T, bool>> condition);

        int UpdateItem(int id, T item);

        int AddItem(T item);

        void DeleteItem(int itemId);
    }
}
