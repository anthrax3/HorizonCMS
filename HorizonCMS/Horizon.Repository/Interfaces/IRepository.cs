using System;
using System.Collections.Generic;

namespace Horizon.Repository.Interfaces
{
    public interface IRepository<T,T1> where T : class                                               
    {
        List<T> GetPaged(Int32 page, Int32 records, string sortField, string sortOrder);

        T Remove(T1 obj);

        T Create(T1 obj);

        T Update(T1 obj);
        
    }
}
