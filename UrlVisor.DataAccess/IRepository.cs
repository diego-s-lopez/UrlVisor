using System;
using System.Collections.Generic;

namespace UrlVisor.DataAccess
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        void Save(T obj);
        T GetById(int id);
        void Delete(T obj);
    }
}
