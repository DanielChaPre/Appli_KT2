using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appli_KT2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        
        EstadosViewModel estadosViewModel = new EstadosViewModel();
        MunicipioViewModel municipiosViewModel;
        ColoniaViewModel coloniaViewModel;
        PerfilPadreViewModel perfil = new PerfilPadreViewModel();
        Estados estadoS;
        bool cp = true;
        private int tipoUsuario;
        public RegisterPage (int tipoUsuario)
		{
            InitializeComponent ();
            this.tipoUsuario = tipoUsuario;
            pEstados.SelectedIndexChanged += OnPickerSelectedIndexChanged;
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            estadoS = (Estados) pEstados.SelectedItem;
            municipiosViewModel = new MunicipioViewModel(estadoS.NombreEstado);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            switch (this.tipoUsuario)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }


            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                while (estadosViewModel.ListEstados.Count != 0)
                {
                    pEstados.ItemsSource = estadosViewModel.ListEstados;
                    pEstados.ItemDisplayBinding = new Binding("NombreEstado");
                    return false;
                }

                return true; // True = Repeat again, False = Stop the timer
            });

            Device.StartTimer(TimeSpan.FromSeconds (5), () =>
            {
                try
                {
                  
                    if (municipiosViewModel.ListMunicipios != null || municipiosViewModel.ListMunicipios.Count != 0)
                    {
                        pMunicipio.ItemsSource = municipiosViewModel.ListMunicipios;
                        pMunicipio.ItemDisplayBinding = new Binding("NombreMunicipio");
                        return false;
                    }
                    return true; // True = Repeat again, False = Stop the timer
                }
                catch (NullReferenceException ex)
                {
                    return true;
                }
               
            });

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                try
                {
                    if (txtCodigoPostal.Text.Length == 5 && cp)
                    {
                        var codPost = txtCodigoPostal.Text;
                        coloniaViewModel = new ColoniaViewModel(codPost);
                        cp = false;
                    }

                    if (coloniaViewModel.ListColonias != null || coloniaViewModel.ListColonias.Count != 0)
                    {
                        pColonia.ItemsSource = coloniaViewModel.ListColonias;
                        pColonia.ItemDisplayBinding = new Binding("NombreColonia");
                        return false;
                    }
                    return true; // True = Repeat again, False = Stop the timer
                }
                catch (NullReferenceException ex)
                {
                    return true;
                }

            });
        }
    }
}