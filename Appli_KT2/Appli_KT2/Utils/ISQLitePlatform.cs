using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Appli_KT2.Utils
{
    public interface ISQLitePlatform
    {
        SQLiteConnection GetConnection();
        SQLiteAsyncConnection GetConnectionAsync();
        //var platform = DependencyService.Get<ISQLitePlatform>();
        //SQLiteConnection db = _platform.GetConnection();

        //db.CreateTable<Item>();
        //db.CreateTable<Stock>();
    }
}
