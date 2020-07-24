using OficinaPAM.Models;
using OficinaPAM.ViewModels.Atendimentos;
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
    public partial class FotoListagemView : ContentPage
    {
        public FotoListagemViewModel viewModel;

        public FotoListagemView(Atendimento atendimento)
        {
            BindingContext = viewModel = new FotoListagemViewModel(atendimento);

            InitializeComponent();
        }
        public FotoListagemView()
        {
            BindingContext = viewModel = new FotoListagemViewModel();

            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.BeginInvokeOnMainThread(async () =>
            {
                await viewModel.ObterAtendimentoFotosAsync();
            });

            MessagingCenter.Subscribe<AtendimentoFoto>(this, "Mostrar", async (foto) => {
                await Navigation.PushAsync(new FotosCRUDView(foto, "Foto Atendimento"));
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<AtendimentoFoto>(this, "Mostrar");
        }
    }
}