using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Services.Repository
{
    public interface IRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetItemsAsync<T>() where T : class, new();
        Task<T> GetItemAsync(int id);
        Task<int> DeleteItemAsync(T item);
        Task<int> SaveItemAsync(T item);
        void UpdateItemAsync(T item);
    }
}
