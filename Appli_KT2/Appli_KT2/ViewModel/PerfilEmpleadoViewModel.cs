using Appli_KT2.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Appli_KT2.ViewModel
{
    public class PerfilEmpleadoViewModel : Empleado
    {
        private Estados _selectedEstado;
        private Municipios _selectedMunicipio;
        private Colonia _selectedColonia;
        private Estados entEstados;
        private bool isrun;

        public bool IsRun
        {
            get { return this.isrun; }
            set
            {
                SetValue(ref this.isrun, value);
            }
        }

        public Estados SelectedEstado
        {
            get
            {
                return _selectedEstado;
            }
            set
            {
                _selectedEstado = value;
                OnPropertyChanged();
                //put here your code  
                //estadoSeleccionado = "City : " + _selectedEstado.NombreEstado;
                Console.WriteLine("Estado recogido:" + _selectedEstado.NombreEstado);
                ObtenerMunicipios();
            }
        }

        public Colonia SelectedColonia
        {
            get
            {
                return _selectedColonia;
            }
            set
            {
                _selectedColonia = value;
                OnPropertyChanged();
            }
        }

        public Municipios SelectedMunicipio
        {
            get
            {
                return _selectedMunicipio;
            }
            set
            {
                _selectedMunicipio = value;
                OnPropertyChanged();
                //put here your code  
                //estadoSeleccionado = "City : " + _selectedEstado.NombreEstado;
                Console.WriteLine("Municipio recogido:" + _selectedEstado.NombreEstado);
            }
        }

        private void ObtenerMunicipios()
        {
            MainViewModel.GetInstance().Municipio = new MunicipioViewModel(this._selectedEstado.NombreEstado);
        }

        #region Commandos

        public ICommand ActualizarPerfilCommand
        {
            get
            {
                return new RelayCommand(ActualizarPerfil);
            }
        }

        public ICommand DesactivarPerfilCommand
        {
            get
            {
                return new RelayCommand(DesactivarPerfil);
            }
        }



        private void DesactivarPerfil()
        {
            throw new NotImplementedException();
        }

        private void ActualizarPerfil()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
