using Appli_KT2.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appli_KT2.Utils
{
    public class MunicipioDataBase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public MunicipioDataBase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Municipios).Name))
                {
                    //await Database.CreateTablesAsync(CreateFlags.None, typeof(Municipios)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Municipios>> GetItemsAsync()
        {
            return Database.Table<Municipios>().ToListAsync();
        }

        public Task<List<Municipios>> GetItemsPlantelAsync(string idPlantel)
        {
            // SQL queries are also possible
            return Database.QueryAsync<Municipios>("SELECT * FROM [Municipios] WHERE [IdPlantelesES] = " + idPlantel + "");
        }

        public Task<List<Municipios>> DeleteAllAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Municipios>("Delete FROM [Municipios]");
        }

        public Task<Municipios> GetItemAsync(int id)
        {
            return Database.Table<Municipios>().Where(i => i.idMunicipio == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Municipios item)
        {
            if (item.idMunicipio != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Municipios item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
