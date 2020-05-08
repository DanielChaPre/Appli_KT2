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
    }
}