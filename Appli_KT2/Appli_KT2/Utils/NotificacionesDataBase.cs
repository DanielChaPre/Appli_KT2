using Appli_KT2.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appli_KT2.Utils
{
    public class NotificacionesDataBase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public NotificacionesDataBase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Notificaciones).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Notificaciones)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Notificaciones>> GetItemsAsync()
        {
            return Database.Table<Notificaciones>().ToListAsync();
        }

        public Task<List<Notificaciones>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Notificaciones>("SELECT * FROM [Notificaciones] WHERE [Done] = 0");
        }

        public Task<Notificaciones> GetItemAsync(int id)
        {
            return Database.Table<Notificaciones>().Where(i => i.Cve_notificacion == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Notificaciones item)
        {
            if (item.Cve_notificacion != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Notificaciones item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
