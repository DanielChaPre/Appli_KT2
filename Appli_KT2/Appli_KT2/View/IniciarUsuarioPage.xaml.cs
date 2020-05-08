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
	public partial class IniciarUsuarioPage : ContentPage
	{
		public IniciarUsuarioPage ()
		{
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
            }
           
		}

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                txtUsuario.Focus();
            }
            catch (Exception ex)
            {
            }
           
        }
    }
}