using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProfileBook.Services.Repository
{
    public class Repository<T>: IRepository<T> where T : class, new()
    {
        private readonly string DATABASE_NAME = "UsersRepository.db";
        public readonly SQLiteAsyncConnection database;
        public Repository()
        {
            database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
            database.CreateTableAsync<T>();          
        }
        public async Task<int> DeleteItemAsync(T item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<T> GetItemAsync(int id)
        {
            return await database.GetAsync<T>(id);
        }
        public async Task<IEnumerable<T>> GetItemsAsync<T>()  where T : class, new()
        {
            return await database.Table<T>().ToListAsync();
        }
        public async Task<int> SaveItemAsync(T item)
        {
            return await database.InsertAsync(item);
        }
        public async void UpdateItemAsync(T item)
        {
            await database.UpdateAsync(item);
        }
        public async Task<User> GetUserByLoginAsync(string userLogin)
        {
            var items = await GetItemsAsync<User>();
            return items.Where(x => x.UserLogin == userLogin).FirstOrDefault();
        }
        public async Task<List<Contact>> GetContactsByIdAsync(int userId)
        {
            var items = await GetItemsAsync<Contact>();
            return items.Where(x => x.UserId == userId).ToList();
        }
    }
}
