using Appli_KT2.Model;
using Appli_KT2.Utils;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class MapsViewModel : INotifyPropertyChanged
    {
        private PlantelesES PlantelesES;
        private List<PlantelesES> _items;
        public SQLiteConnection conn;

        public List<PlantelesES> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public MapsViewModel(PlantelesES plantelesES)
        {
            Init();
        }

        private void Init()
        {
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            var idPlantel = App.Current.Properties["idPlnatelES"].ToString();
            var listaImagenes = conn.Query<PlantelesES>("SELECT * FROM PlantelesES WHERE idPlantelES = ?",""+idPlantel+"");
            Items = new List<PlantelesES>();
            Items.Add(listaImagenes[0]);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
