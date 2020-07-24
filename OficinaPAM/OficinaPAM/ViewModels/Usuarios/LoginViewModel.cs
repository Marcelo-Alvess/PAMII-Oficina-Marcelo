using OficinaPAM.Models;
using OficinaPAM.Servicos.Clientes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OficinaPAM.ViewModels.Usuarios
{
    public class LoginViewModel : BaseViewModel
    {
        private IClienteService cService = new ClienteService();

        private Cliente Usuario;
        public ICommand EntrarCommand { get; set; }

        public async Task ConsultarUsuario()
        {
            ObservableCollection<Cliente> Clientes = await cService.GetClientesAsync();

            Cliente cli = Clientes.ToList().Find(x => x.EMail == Usuario.EMail && x.Telefone == Usuario.Telefone);

            if (cli == null)
                cli = new Cliente();

            
            MessagingCenter.Send<Cliente>(cli, "InformacaoCRUD");
            
        }

        private void RegistrarCommands()
        {
            EntrarCommand = new Command(async () => 
            {
                await ConsultarUsuario();
            });
        }

        public LoginViewModel()
        {
            this.Usuario = new Cliente();
            RegistrarCommands();
        }

        public string User
        {
            get { return this.Usuario.EMail; }
            set
            {
                this.Usuario.EMail = value;
                OnPropertyChanged();
            }
        }

        public string Senha
        {
            get { return this.Usuario.Telefone; }
            set
            {
                this.Usuario.Telefone = value;
                OnPropertyChanged();
            }
        }
    }
}
