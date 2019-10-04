using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Appli_KT2.Model
{
    public class EscuelasClass
    {
        public EscuelasClass()
        {
           
        }
        public ObservableCollection<MenuEjemplo1> ObtenerMenuEjemplo1()
            {
            ObservableCollection<MenuEjemplo1> oMenuPrincipal = new ObservableCollection<MenuEjemplo1>();

            oMenuPrincipal.Add(new MenuEjemplo1
            {
                Opcion = "Universidad Tecnologíca de León",
                Habilitado = true,
                idOpcion = 1,
                icon = "utl.png",
                Direccion = "Blvd. Universidad Tecnológica 225, Universidad Tecnologica, San Carlos la Roncha, 37670 León, Gto.",
                Web = "http://www.utleon.edu.mx/"

            });
            oMenuPrincipal.Add(new MenuEjemplo1
            {
                Opcion = "Instituto Tecnologíco de León",
                Habilitado = true,
                idOpcion = 2,
                icon = "itleon.png",
                Direccion = "Montadores S/n, Industrial Julian de Obregon, 37290 León, Gto.",
                Web = "https://www.itleon.edu.mx/"
            });
            oMenuPrincipal.Add(new MenuEjemplo1
            {
                Opcion = "Universidad de Guanajuato",
                Habilitado = true,
                idOpcion = 3,
                icon = "ug.png",
                Direccion = "Lascurain de Retana No. 5; Ciudad de Guanajuato, Guanajuato, México",
                Web = "http://www.ugto.mx/",
            });
            return oMenuPrincipal;


        }
    }
}
