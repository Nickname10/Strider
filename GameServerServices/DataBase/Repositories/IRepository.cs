using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServerServices.DataBase.Repositories
{
    interface IRepository<T>
        where T : class
    {
        IQueryable<T> Get();
        void Add(T item);
        void Update(int itemId, T newItem);
        Task UpdateAsync(int itemId, T newItem);
        void Remove(int itemId);
        Task RemoveAsync(int itemId);
    }
}