using OficinaPAM.Views;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OficinaPAM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navigationPage = new Views.Usuarios.LoginView();

            MainPage = navigationPage;
        }

        //MainPage = new Xamarin.Forms.NavigationPage(new TabbedPageView());
        //CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
        //CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
