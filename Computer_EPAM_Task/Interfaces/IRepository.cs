using System.Collections.Generic;
using System.Threading.Tasks;

namespace Computer_EPAM_Task.Interfaces
{
    internal interface IRepository<T> where T : class
    {
        Task<bool> TryCreateAsync(T item);

        Task<bool> TryUpdateAsync(T item);

        Task<bool> TryDeleteAsync(T item);

        Task<T> TryGetAsync(int id);

        Task<IEnumerable<T>> TryGetAllAsync();
    }
}