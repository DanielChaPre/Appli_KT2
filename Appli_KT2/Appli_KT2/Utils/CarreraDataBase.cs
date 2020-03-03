using Appli_KT2.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appli_KT2.Utils
{
    public class CarreraDataBase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public CarreraDataBase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(CarrerasES).Name))
                {
                    //await Database.CreateTablesAsync(CreateFlags.None, typeof(CarrerasES)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<CarrerasES>> GetItemsAsync()
        {
            return Database.Table<CarrerasES>().ToListAsync();
        }

        public Task<List<CarrerasES>> GetItemsPlantelAsync(string idPlantel)
        {
            // SQL queries are also possible
            return Database.QueryAsync<CarrerasES>("SELECT * FROM [CarrerasES] WHERE [IdPlantelesES] = "+idPlantel+"");
        }

        public Task<List<CarrerasES>> DeleteAllAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<CarrerasES>("Delete FROM [CarrerasES]");
        }

        public Task<CarrerasES> GetItemAsync(int id)
        {
            return Database.Table<CarrerasES>().Where(i => i.idCarreraES == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(CarrerasES item)
        {
            if (item.idCarreraES != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(CarrerasES item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
