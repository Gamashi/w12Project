using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w12.Models;

namespace w12.Services
{
    public class Database
    {
        SQLiteAsyncConnection database;

        public async Task Init()
        {
            if (database is not null)
                return;

            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await database.CreateTableAsync<BaseExercise>();
        }

        public async Task<List<BaseExercise>> GetItemsAsync()
        {
            await Init();
            return await database.Table<BaseExercise>().ToListAsync();
        }

        //public async Task<List<TodoItem>> GetItemsNotDoneAsync()
        //{
        //    await Init();
        //    return await database.Table<TodoItem>().Where(t => t.Done).ToListAsync();

        //    // SQL queries are also possible
        //    //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        //}

        public async Task<BaseExercise> GetItemAsync(int id)
        {
            await Init();
            return await database.Table<BaseExercise>().Where(i => i.BaseExerciseId == id).FirstOrDefaultAsync();
        }

        //public async Task<int> SaveItemAsync(TodoItem item)
        //{
        //    await Init();
        //    if (item.ID != 0)
        //        return await database.UpdateAsync(item);
        //    else
        //        return await database.InsertAsync(item);
        //}

        //public async Task<int> DeleteItemAsync(TodoItem item)
        //{
        //    await Init();
        //    return await database.DeleteAsync(item);
        //}
    }
}
