﻿using Appli_KT2.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Appli_KT2.Utils
{
    public class DetallePlantelDataBase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        SQLiteConnection db;
        static bool initialized = false;

        public DetallePlantelDataBase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            try
            {
                if (!initialized)
                {
                    if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(DetallePlantel).Name))
                    {
                        db = new SQLiteConnection(Database.DatabasePath);
                        db.CreateTable<PlantelesES>();
                        db.CreateTable<DetallePlantel>();

                        //await Database.CreateTablesAsync(CreateFlags.None, typeof(PlantelesES)).ConfigureAwait(false);
                        //await Database.CreateTablesAsync(CreateFlags.None, typeof(DetallePlantel)).ConfigureAwait(false);
                        initialized = true;
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
            }

        }

        public Task<List<DetallePlantel>> GetItemsAsync()
        {
            return Database.Table<DetallePlantel>().ToListAsync();
        }

        public Task<List<DetallePlantel>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<DetallePlantel>("SELECT * FROM [DetallePlantel] WHERE [Done] = 0");
        }

        public Task<List<DetallePlantel>> DeleteAllAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<DetallePlantel>("Delete FROM [DetallePlantel]");
        }

        public Task<DetallePlantel> GetItemAsync(int id)
        {
            return Database.Table<DetallePlantel>().Where(i => i.Cve_detalle_plantel == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(DetallePlantel item)
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

        public Task<int> DeleteItemAsync(DetallePlantel item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
