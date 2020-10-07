using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProfileBook.Services.Repository
{
    //как хранить две таблицы
    //где писать запросы??????????????????????????????
    class Repository
    {
        SQLiteAsyncConnection database;

        public Repository(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
        }

        public async Task CreateTable()
        {
            await database.CreateTableAsync<User>();
        }
        public async Task<List<User>> GetItemsAsync()
        {
            return await database.Table<User>().ToListAsync();

        }
        public async Task<User> GetItemAsync(int id)
        {
            return await database.GetAsync<User>(id);
        }
        public async Task<int> DeleteItemAsync(User item)
        {
            return await database.DeleteAsync(item);
        }
        public async Task<int> SaveItemAsync(User item)
        {
            if (item.UserId != 0)
            {
                await database.UpdateAsync(item);
                return item.UserId;
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }
    }
}
