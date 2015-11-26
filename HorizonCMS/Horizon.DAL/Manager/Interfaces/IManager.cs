
using System.Collections.Generic;
namespace Horizon.DAL.Manager.Interfaces
{
    public interface IManager<T> where T : Tables.Interfaces.ITable
    {
        T Create(T obj);

        List<T> Retrieve(int page, int records, string sortField, string SortOrder);

        T Update(T obj);

        bool Delete(T obj);

    }
}
