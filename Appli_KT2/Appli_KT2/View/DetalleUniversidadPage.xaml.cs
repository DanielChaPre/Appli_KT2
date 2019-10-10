using Appli_KT2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Appli_KT2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleUniversidadPage : ContentPage
	{
        public List<CarouselModel> MyDataSource { get; set; }
        private int _position;
        public int Position { get { return _position; } set { _position = value; OnPropertyChanged(); } }
        public DetalleUniversidadPage ()
		{
			InitializeComponent ();
            MyDataSource = new List<CarouselModel>() { new CarouselModel() { imagen = "utl.png"},
                                                        new CarouselModel() { imagen="ug.png" },
                                                        new CarouselModel() { imagen = "itleon.png"}};

            BindingContext = this;
            MyMap.MoveToRegion(
            MapSpan.FromCenterAndRadius(
                new Position(21.063605, -101.581646), Distance.FromMiles(1)));
        }
	}
}