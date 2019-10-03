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
                icon = "utl.png"
            });
            oMenuPrincipal.Add(new MenuEjemplo1
            {
                Opcion = "Instituto Tecnologíco de León",
                Habilitado = true,
                idOpcion = 2,
                icon = "itleon.png"
            });
            oMenuPrincipal.Add(new MenuEjemplo1
            {
                Opcion = "Universidad de Guanajuato",
                Habilitado = true,
                idOpcion = 3,
                icon = "ug.png"
            });
            /*  oMenuPrincipal.Add(new MenuEjemplo1
              {
                  Opcion = "ListView Ejemplo 4",
                  Habilitado = true,
                  idOpcion = 4,
                  icon = "ic_snorlax.png"
              });
              oMenuPrincipal.Add(new MenuEjemplo1
              {
                  Opcion = "ListView Ejemplo 5",
                  Habilitado = true,
                  idOpcion = 5,
                  icon = "ic_squirtle.png"
              });*/


            return oMenuPrincipal;


        }
    }
}
