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
            this.MainPage = new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Color.Navy,
                BarTextColor = Color.White,
            };
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
