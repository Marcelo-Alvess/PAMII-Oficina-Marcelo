using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OficinaPAM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageView : MasterDetailPage
    {
        public MainPageView()
        {
            InitializeComponent();
            masterPage.ListView.ItemSelected += ListView_ItemSelected;            
            IsPresented = true;

            //Página de Detail está sendo atribuída no Constutor da View. 
            //TabbedPageView será a View Inicial de navegação.
            var navigationPage = new Xamarin.Forms.NavigationPage(new Views.TabbedPageView());
            
            //Atribuição sendo enviada para Tag Detail
            this.Detail = navigationPage;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Models.MenuItem;

            if (item == null)
                return;

            var page = (Xamarin.Forms.Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;
            Detail = new Xamarin.Forms.NavigationPage(page);
            
            IsPresented = false;
            masterPage.ListView.SelectedItem = null;
        }

        //navigationPage.On<Xamarin.Forms.PlatformConfiguration.iOS>().SetPrefersLargeTitles(true);
        //(Detail as Xamarin.Forms.NavigationPage).On<Xamarin.Forms.PlatformConfiguration.iOS>().SetPrefersLargeTitles(true);
    }
}