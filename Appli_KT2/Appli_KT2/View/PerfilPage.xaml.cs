using Appli_KT2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appli_KT2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        PerfilPadreViewModel rvm;
		public RegisterPage ()
		{
            InitializeComponent ();
            //MainViewModel.GetInstance().Registrar = new RegistrarViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //MainViewModel.GetInstance().Registrar = new RegistrarViewModel();
            //Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}