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
	public partial class CreateAccountPage : ContentPage
	{
        public CreateAccountPage()
        {
            InitializeComponent();
            // lblUsuario.TranslateTo(0, 5, 0);
            //txtUsuario.Focused += animacionUsu;
            //txtContraseña.Focused += animacionPass;
        }

        private void animacionPass(object sender, FocusEventArgs e)
        {
            if (txtContraseña.IsFocused)
            {
                lblContraseña.TranslateTo(0, 3, 100);
                lblUsuario.TranslateTo(0, 40, 100);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           // txtUsuario.Focus();
        }
        public void animacionUsu(object sender, FocusEventArgs e)
        {
            if (txtUsuario.IsFocused)
            {
                lblUsuario.TranslateTo(0, 3, 100);
                lblContraseña.TranslateTo(0, 40, 100);
            }
            
        }
    }
}