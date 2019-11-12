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
        RegistrarViewModel rvm;
		public RegisterPage ()
		{
         //  
          //  MainViewModel.GetInstance().Registrar = new RegistrarViewModel();
            InitializeComponent ();
          //  MainViewModel.GetInstance().Registrar = new RegistrarViewModel();
           this.rvm = new RegistrarViewModel();
            pckPerfiles.ItemsSource = rvm.Perfiles;
            BindingContext = rvm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           
         //   this.rvm = new RegistrarViewModel();
        }
    }
}