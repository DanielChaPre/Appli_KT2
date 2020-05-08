using Appli_KT2.Droid;
using Appli_KT2.Services;
using PdfSharpCore.Pdf;
using System.IO;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(PdfSave))]
namespace Appli_KT2.Droid
{
    public class PdfSave : IPdfSave
    {
        public void Save(PdfDocument doc, string fileName)
        {
            //  string path = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + fileName);
             string path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);
            //  string path = System.IO.Path.Combine(fileName);
            //  string path = System.IO.Path.Combine("/Android/data/com.companyname.Appli_KT/files/"+ fileName);
            // Get the path to a file on internal storage
           // var backingFile = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "count.txt");

            // Get the path to a file in the cache directory
           // var cacheFile = Path.Combine(Xamarin.Essentials.FileSystem.CacheDirectory, "count.txt");

            doc.Save(path);
           // doc.Save(cacheFile);
            doc.Close();

            Share.RequestAsync(new ShareFileRequest
            {
                Title = "Prueba",
                File = new ShareFile(path)
            });
            global::Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                title: "Éxito",
                message: $"Su PDF se ha generado y se a guardado en @{path}",
                cancel: "Acpetar");
        }
    }
}