﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
    public class Estados : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int idEstado;
        private string nombreEstado;
        private int idPais;

        public int IdEstado
        {
            get
            {
                return idEstado;
            }
            set
            {
                idEstado = value;
                OnPropertyChanged();
            }
        }
        public string NombreEstado
        {
            get
            {
                return nombreEstado;
            }
            set
            {
                nombreEstado = value;
                OnPropertyChanged();
            }
        }
        public int IdPais
        {
            get
            {
                return idPais;
            }
            set
            {
                idPais = value;
                OnPropertyChanged();
            }
        }
    }
}
