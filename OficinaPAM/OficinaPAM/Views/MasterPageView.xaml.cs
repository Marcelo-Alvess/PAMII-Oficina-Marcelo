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
    public partial class MasterPageView : ContentPage
    {
        public Models.MenuItem[] OpcoesMenu { get; set; }
        public ListView ListView { get; set; }
        
        //Moficar o construtor já existente

        public string DadosUsuario
        {
            get
            {
                string nome = string.Empty;
                string id = string.Empty;

                if (Application.Current.Properties.ContainsKey("UsuarioId"))
                    id = Application.Current.Properties["UsuarioId"].ToString();

                if (Application.Current.Properties.ContainsKey("UsuarioNome"))
                    nome = Application.Current.Properties["UsuarioNome"].ToString();

                return string.Format("Usuário: {0} (Id {1})", nome, id);
            }
        }

        public MasterPageView()
        {
            Icon = "OficinaApp.png";
            InitializeComponent();

            OpcoesMenu = new[]
            {
                new Models.MenuItem
                {
                    Id = 0,
                    Title = "Clientes",
                    TargetType = typeof(Views.Clientes.ListagemView),
                    IconSource ="Clientes.png"
                },

                new Models.MenuItem {
                    Id = 1,
                    Title = "Listagem ",
                    TargetType = typeof(Views.TabbedPageView),
                    IconSource ="Servicos.png"
                },

                new Models.MenuItem {
                    Id = 2,
                    Title = "Fotos Registradas",
                    TargetType = typeof(Views.Atendimentos.FotoListagemView),
                    IconSource ="Add.png"
                },

                new Models.MenuItem {
                    Id = 4,
                    Title = "Atendimentos",
                    TargetType = typeof(Views.Atendimentos.AtendimentoListagemView),
                    IconSource ="Atendimentos.png"
                }                
            };
            ListView = itensMenuListView;
            BindingContext = this;
        }
    }
}

