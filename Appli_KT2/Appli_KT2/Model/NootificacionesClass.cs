using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Appli_KT2.Model
{
    public class NootificacionesClass
    {
        public NootificacionesClass()
        {

        }
        public ObservableCollection<NotificacionesMClass> ObtenerMenuEjemplo1()
        {
            ObservableCollection<NotificacionesMClass> oMenuPrincipal = new ObservableCollection<NotificacionesMClass>();

            oMenuPrincipal.Add(new NotificacionesMClass
            {
                Categoria = "Oferta Educativa",
                Titulo = "Nueva carrera en la UTL",
                icon = "ic_noti_oe.png",
                Habilitado = true,
                idOpcion = 1
            });
            oMenuPrincipal.Add(new NotificacionesMClass
            {
                Categoria = "SUREDSU",
                Titulo = "Nueva carrera en Tecnológias",
                icon = "ic_noti_s.png",
                Habilitado = true,
                idOpcion = 2
            });
            oMenuPrincipal.Add(new NotificacionesMClass
            {
                Categoria = "EDUCAFIN",
                Titulo = "Beca Avanza",
                icon = "ic_noti_e.png",
                Habilitado = true,
                idOpcion = 3
            });
            oMenuPrincipal.Add(new NotificacionesMClass
            {
                Categoria = "SDES",
                Titulo = "Vacante en la UTL",
                icon = "ic_noti_s.png",
                Habilitado = true,
                idOpcion = 4
            });
            oMenuPrincipal.Add(new NotificacionesMClass
            {
                Categoria = "Aula Virtual",
                Titulo = "Curso de programación",
                icon = "ic_noti_au.png",
                Habilitado = true,
                idOpcion = 5
            });
            oMenuPrincipal.Add(new NotificacionesMClass
            {
                Categoria = "Internacionalización",
                Titulo = "Beca a Francia",
                icon = "ic_noti_i.png",
                Habilitado = true,
                idOpcion = 6
            });
            oMenuPrincipal.Add(new NotificacionesMClass
            {
                Categoria = "Otros eventos",
                Titulo = "Congreso de ecología",
                icon = "ic_noti_oe.png",
                Habilitado = true,
                idOpcion = 7
            });
            return oMenuPrincipal;
        }
    }
}
