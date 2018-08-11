using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(int id);
        T Get(int Id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        RepositoryActionResult<T> Create(T item);
        RepositoryActionResult<T> Update(T item);
        RepositoryActionResult<T> Delete(int id);
    }
}
