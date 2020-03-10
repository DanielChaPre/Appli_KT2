using Appli_KT2.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Appli_KT2.Utils
{
    public class SQLiteDataBase
    {
        public SQLiteConnection conn;
        public void CrearTablas()
        {
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            conn.CreateTable<DetallePlantel>();
            conn.CreateTable<PlantelesES>();
            conn.CreateTable<Municipios>();
            conn.CreateTable<ImagenPlantel>();
        }
    }
}
