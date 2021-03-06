﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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
    }
}
