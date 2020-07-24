using OficinaPAM.Models;
using OficinaPAM.ViewModels.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OficinaPAM.Views.Clientes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PesquisaView : ContentPage
    {
        private PesquisaViewModel viewModel;
        public PesquisaView()
        {
            InitializeComponent();
            viewModel = new PesquisaViewModel();
            BindingContext = viewModel;
            ClienteSelecionado = null;
        }
        public static Cliente ClienteSelecionado { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Cliente>(this, "ClienteSelecionado", (cliente) =>
            {
                ClienteSelecionado = cliente;
                Navigation.PopAsync();
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<string>(this, "ClienteSelecionado");
        }
    }
}