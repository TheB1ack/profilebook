using ProfileBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Services.Repository
{
    public interface IRepository<T> where T : class, new()
    {
        Task<List<T>> GetItems();
        Task<T> GetItemAsync(int id);
        Task<int> DeleteItemAsync(T item);
        Task<int> SaveItemAsync(T item);
        User GetUserByLogin(string userLogin);
        void UpdateItemAsync(T item);
        List<Contact> GetContactsById(int userId);
    }
}
