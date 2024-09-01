using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampRcon.Models;
using SQLite;

namespace SampRcon.Utils.SQL
{
    public class ItemDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public ItemDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Server).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Server)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Server>> GetItemsAsync()
        {
            return Database.Table<Server>().ToListAsync();
        }

        public Task<Server> GetItemAsync(int id)
        {
            return Database.Table<Server>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Server server)
        {
            var servers = GetItemsAsync().Result;
            var serverExist = servers.Where(x => x.IP == server.IP).Any();

            if (serverExist)
            {
                return Database.UpdateAsync(server);
            }
            else
            {
                return Database.InsertAsync(server);
            }
        }

        public Task<int> DeleteItemAsync(Server server)
        {
            return Database.DeleteAsync(server);
        }

        public Task<int> DeleteAllAsync()
        {
            return Database.DeleteAllAsync<Server>();
        }
    }
}