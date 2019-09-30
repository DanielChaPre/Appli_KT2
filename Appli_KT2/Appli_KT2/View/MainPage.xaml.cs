using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appli_KT2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageMenuItem;
            if (item == null)
                return;

            switch (item.Id)
            {
                case 0:
                    Detail = new NavigationPage(new AtlasPage());
                    IsPresented = false;

                    MasterPage.ListView.SelectedItem = null;
                    break;
                case 1:
                    Detail = new NavigationPage(new NotificacionesPage());
                    IsPresented = false;

                    MasterPage.ListView.SelectedItem = null; 
                    break;
                case 2:
                    Navigation.PushAsync(new SuredsuPage());
                    break;
                case 3:
                    Navigation.PushAsync(new SuredsuPage());

                    break;
                case 4:
                    Navigation.PushAsync(new SuredsuPage());
                    break;
                default:
                    break;
            }
             /*  var page = (Page)Activator.CreateInstance(item.TargetType);
               page.Title = item.Title;

               Detail = new NavigationPage(page);
               IsPresented = false;

               MasterPage.ListView.SelectedItem = null;*/
        }
    }
}