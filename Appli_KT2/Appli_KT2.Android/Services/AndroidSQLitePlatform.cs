using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Appli_KT2.Droid.Services;
using Appli_KT2.Utils;
using SQLite;
[assembly: Xamarin.Forms.Dependency(typeof(AndroidSQLitePlatform))]

namespace Appli_KT2.Droid.Services
{
   
    public class AndroidSQLitePlatform : ISQLitePlatform
    {

        public SQLiteConnection GetConnection()
        {
            var dbase = "Applikt";
            var dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(dbpath, dbase);
            var connection = new SQLiteConnection(path);
            return connection;

        }
        //private string GetPath()
        //{
        //    var dbName = "Applikt.db3";
        //    var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
        //    return path;
        //}
        //public SQLiteConnection GetConnection()
        //{
        //    return new SQLiteConnection(GetPath());
        //}
        //public SQLiteAsyncConnection GetConnectionAsync()
        //{
        //    return new SQLiteAsyncConnection(GetPath());
        //}
    }
}