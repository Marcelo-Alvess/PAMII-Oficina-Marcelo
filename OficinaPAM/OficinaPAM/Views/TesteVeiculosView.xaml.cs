using OficinaPAM.Models;
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
    public partial class TesteVeiculosView : ContentPage
    {
        public ClienteTeste Cliente { get; private set; }
        public TesteVeiculosView()
        {
            InitializeComponent();
        }
        public TesteVeiculosView(ClienteTeste cliente)
        {
            InitializeComponent();

            Title = $"Veículos de {cliente.Nome}";
            listViewVeiculos.ItemsSource = cliente.Veiculos;
            this.Cliente = cliente;
        }
    }

}