using Appli_KT2.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Appli_KT2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
           // pruebaShared();
            
        }



        protected override void OnStart()
        {
          this.MainPage = new NavigationPage(new SplashPage()); 
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
