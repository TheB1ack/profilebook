using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProfileBook.Services.Repository
{
    public class Repository: IRepository<User>
    {
        //как проще вурнуть айди добавленного пользователя
        //как парвильно искать логин в базе
        //как между страницами передавать репозиторий, если объект репозитория должен быть один 
        private const string DATABASE_NAME = "UsersRepository.db";

        public readonly SQLiteAsyncConnection database;
        public Repository()
        {
           database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
           database.CreateTableAsync<User>().Wait();
        }
        public async Task<int> DeleteItemAsync(User item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<User> GetItemAsync(int id)
        {
            return await database.GetAsync<User>(id);
        }
        public async Task<List<User>> GetItemsAsync()
        {
            return await database.Table<User>().ToListAsync();
        }
        public async Task<int> SaveItemAsync(User item)
        {
            return await database.InsertAsync(item);
        }
        public User GetItemByLogin(string userLogin)
        {
            var user =  from u 
                        in database.Table<User>()
                        where u.UserLogin == userLogin
                        select u;
            var r = user.ToListAsync().Result;
            return r.Count == 0? null : r[0];
        }
        public async void UpdateItemLogged(string userLogin, bool value)
        {
            var user = GetItemByLogin(userLogin);
            //user.IsLoggedIn = value;
            await database.UpdateAsync(user);
        }
    }
}
