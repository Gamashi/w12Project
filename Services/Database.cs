using SQLite;
using w12.Models;

namespace w12.Services
{
    public class Database
    {
        SQLiteAsyncConnection database;
        public async Task Init()
        {
            if (database is not null)
            {
                return;
            }

            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            
            await InitCategories();
            var result = await database.CreateTableAsync<BaseExercise>();

        }
        private async Task InitCategories( )
        {
            var result2 = await database.CreateTableAsync<Category>();

            var existing = await database.Table<Category>().FirstOrDefaultAsync();
            if (existing == null)
            {
                var baseExercises = new List<Category>
                {
                    new Category { Name = "Peito"},
                    new Category { Name = "Costas" },
                    new Category { Name = "Pernas" },
                    new Category { Name = "Ombros" },
                    new Category { Name = "Bíceps" },
                    new Category { Name = "Tríceps" },
                    new Category { Name = "Glúteos" },
                    new Category { Name = "Panturrilha" },
                    new Category { Name = "Abdômen" }
                };
                await database.InsertAllAsync(baseExercises);
            }
        }
        public async Task<List<BaseExercise>> GetItemsAsync()
        {
            await Init();
            return await database.Table<BaseExercise>().ToListAsync();
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            await Init();
            return await database.Table<Category>().ToListAsync();
        }

        public async Task<List<BaseExercise>> GetBaseExercisesAsync()
        {
            await Init();
            return await database.Table<BaseExercise>().ToListAsync();
        }
        public async Task<BaseExercise> GetExercise()
        {
            await Init();
            return await database.Table<BaseExercise>().FirstOrDefaultAsync();
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

        public async Task<int> SaveBaseExercise(BaseExercise baseExercise)
        {
            await Init();
            if (baseExercise.BaseExerciseId != 0)
                return await database.UpdateAsync(baseExercise);
            else
                return await database.InsertAsync(baseExercise);
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
