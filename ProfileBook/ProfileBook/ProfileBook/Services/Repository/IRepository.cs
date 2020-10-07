using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Repository
{
    interface IRepository
    {
        Task CreateTable();
        Task<List<User>> GetItemsAsync();
        Task<User> GetItemAsync(int id);
        Task<int> DeleteItemAsync(User item);
        Task<int> SaveItemAsync(User item);
    }
}
