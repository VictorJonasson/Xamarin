using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.DbConnection
{
    public class SQLiteDB
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public SQLiteDB()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(TodoModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(TodoModel)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        public Task<List<TodoModel>> GetItemsAsync()
        {
            return Database.Table<TodoModel>().ToListAsync();
        }

        public Task<List<TodoModel>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<TodoModel>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public Task<TodoModel> GetItemAsync(int id)
        {
            return Database.Table<TodoModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TodoModel item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TodoModel item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
