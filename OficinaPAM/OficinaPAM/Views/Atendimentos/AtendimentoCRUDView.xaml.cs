using OficinaPAM.Models;
using OficinaPAM.ViewModels.Atendimentos;
using OficinaPAM.Views.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OficinaPAM.Views.Atendimentos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AtendimentoCRUDView : ContentPage
    {
        public AtendimentoCRUDView()
        {
            InitializeComponent();
        }

        private AtendimentoCRUDViewModel atendimentoCrudViewModel;
        public AtendimentoCRUDView(Atendimento atendimento, string title) : this()
        {
            this.atendimentoCrudViewModel = new AtendimentoCRUDViewModel(atendimento);
            this.BindingContext = this.atendimentoCrudViewModel;
            this.Title = title;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (PesquisaView.ClienteSelecionado != null)
                atendimentoCrudViewModel.Cliente = PesquisaView.ClienteSelecionado;

            MessagingCenter.Subscribe<Atendimento>(this, "MostrarPesquisaCliente", async (atendimento) =>
            {
                await Navigation.PushAsync(new PesquisaView());
            });

            MessagingCenter.Subscribe<string>(this, "InformacaoCRUD", async (msg) =>
            { await DisplayAlert("Informação", msg, "OK"); });

            MessagingCenter.Subscribe<Atendimento>(this, "MostrarFotos", async (atendimento) => 
            {
                await Navigation.PushAsync(new FotoListagemView(atendimento));
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Atendimento>(this, "MostrarPesquisaCliente");
            MessagingCenter.Unsubscribe<Atendimento>(this, "InformacaoCRUD");
            MessagingCenter.Unsubscribe<Atendimento>(this, "MostrarFotos");
        }



    }
}


