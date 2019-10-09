using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Appli_KT2.ViewModel
{
    public class ShareDialogClass
    {
        /*public interface IShare
        {
            //Task Show(string title, string message, string filePath);
            Task Show(string title, string message);
        }*/
        public async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }

        public async Task ShareUri(string uri, string title)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = title
            });
        }
    }
}
