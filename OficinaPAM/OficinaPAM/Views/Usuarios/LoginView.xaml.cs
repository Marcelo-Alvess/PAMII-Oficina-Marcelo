using OficinaPAM.Models;
using OficinaPAM.ViewModels.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OficinaPAM.Views.Usuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        LoginViewModel loginViewModel;
        Cliente c;
        public LoginView()
        {
            InitializeComponent();
            c = new Cliente();
            loginViewModel = new LoginViewModel();
            BindingContext = loginViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Cliente>(this, "InformacaoCRUD", async (c) => 
            {
                if (c.Id != 0)
                {
                    Application.Current.Properties["UsuarioId"] = c.Id;
                    Application.Current.Properties["UsuarioNome"] = c.Nome;

                    string mensagem = string.Format("Bem-Vindo {0}", c.Nome);
                    await DisplayAlert("Informação", mensagem, "OK");
                    await Navigation.PushModalAsync(new MainPageView());
                }
                else
                    await DisplayAlert("Informação", "Dados incorretos!!! =(", "OK");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "InformacaoCRUD");
        }
    }
}