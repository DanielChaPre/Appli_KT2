using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Appli_KT2.Model
{
    public class CalificacionesClass
    {
        public CalificacionesClass()
        {

        }
        public ObservableCollection<MCalificacionesClass> ObtenerMenuEjemplo1()
        {
            ObservableCollection<MCalificacionesClass> oMenuPrincipal = new ObservableCollection<MCalificacionesClass>();

            oMenuPrincipal.Add(new MCalificacionesClass
            {
                Titulo = "Calificaciones semestre 5 Conalep",
                Habilitado = true,
                idOpcion = 1

            });
            oMenuPrincipal.Add(new MCalificacionesClass
            {
                Titulo = "Calificaciones semestre 6 Conalep",
                Habilitado = true,
                idOpcion = 1

            });
            oMenuPrincipal.Add(new MCalificacionesClass
            {
                Titulo = "Calificaciones Cuatrimestre 1 UTL",
                Habilitado = true,
                idOpcion = 1

            });
            oMenuPrincipal.Add(new MCalificacionesClass
            {
                Titulo = "Calificaciones Cuatrimestre 2 UTL",
                Habilitado = true,
                idOpcion = 1

            });
            oMenuPrincipal.Add(new MCalificacionesClass
            {
                Titulo = "Calificaciones Cuatrimestre 3 UTL",
                Habilitado = true,
                idOpcion = 1

            });
           
            return oMenuPrincipal;


        }
    }
}
