using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileBook.Services.Repository
{
    public interface IRepository<User>
    {
        List<User> GetItems();
        Task<User> GetItemAsync(int id);
        Task<int> DeleteItemAsync(User item);
        Task<int> SaveItemAsync(User item);
        User GetItemByLogin(string userLogin);
    }
}
