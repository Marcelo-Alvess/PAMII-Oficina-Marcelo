using OficinaPAM.Models;
using OficinaPAM.Servicos;
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
    public partial class TesteClientesView : ContentPage
    {
        public TesteClientesView()
        {
            InitializeComponent();
            listViewClientes.ItemsSource = new DadosParaTeste().Clientes;
        }

        private async void listViewClientes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var cliente = e.SelectedItem as ClienteTeste;
            await Navigation.PushAsync(new TesteVeiculosView(cliente));
            ((ListView)sender).SelectedItem = null;
        }
    }

}