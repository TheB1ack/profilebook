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
        public async Task<List<T>> GetItems() //асинхронность?
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
        public User GetUserByLogin(string userLogin)
        {
            var user =  from u 
                        in database.Table<User>() //асинхронность?
                        where u.UserLogin == userLogin
                        select u;
            var r = user.ToListAsync().Result; //асинхронность?
            return r.Count == 0? null : r[0];
        }
        public List<Contact> GetContactsById(int userId)
        {
            var contacts = from c 
                           in database.Table<Contact>() //асинхронность?
                           where c.UserId == userId
                           select c;
            var r = contacts.ToListAsync().Result; //асинхронность? //зависает
            return r.Count == 0 ? null : r; ;
        }
    }
}
