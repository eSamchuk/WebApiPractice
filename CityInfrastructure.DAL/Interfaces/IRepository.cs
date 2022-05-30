using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CityInfrastructure.DAL
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetFullList();

        T GetItemById(int id);

        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        void DeleteItem(int id);

        void AddItem(T item);

        void UpdateItem(T item);


    }
}
