using Appli_KT2.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appli_KT2.Utils
{
    public class ImagenPlantelDataBase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public ImagenPlantelDataBase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ImagenPlantel).Name))
                {
                   // await Database.CreateTablesAsync(CreateFlags.None, typeof(ImagenPlantel)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<ImagenPlantel>> GetItemsAsync()
        {
            return Database.Table<ImagenPlantel>().ToListAsync();
        }

        public Task<List<ImagenPlantel>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<ImagenPlantel>("SELECT * FROM [ImagenPlantel] WHERE [Done] = 0");
        }

        public Task<List<ImagenPlantel>> DeleteAllAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<ImagenPlantel>("Delete FROM [ImagenPlantel]");
        }

        public Task<ImagenPlantel> GetItemAsync(int id)
        {
            return Database.Table<ImagenPlantel>().Where(i => i.Cve_detalle_plantel == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ImagenPlantel item)
        {
            if (item.Cve_detalle_plantel != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(ImagenPlantel item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
