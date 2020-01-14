﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
    public class Empleado : Persona, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int cve_empleado;
        private string numero_empleado;
        private string estatus;
        private DateTime fecha_registro;
        private Persona personaE;

        public int Cve_empleado
        {
            get { return cve_empleado; }
            set { cve_empleado = value;
                OnPropertyChanged();
            }
        }
        public string Numero_empleado
        {
            get { return numero_empleado; }
            set
            {
                numero_empleado = value;
                OnPropertyChanged();
            }
        }
        public string Estatus
        {
            get { return estatus; }
            set
            {
                estatus = value;
                OnPropertyChanged();
            }
        }
        public DateTime Fecha_registro
        {
            get { return fecha_registro; }
            set
            {
                fecha_registro = value;
                OnPropertyChanged();
            }
        }
        public Persona PersonaE
        {
            get { return personaE; }
            set
            {
                personaE = value;
                OnPropertyChanged();
            }
        }
    }
}
