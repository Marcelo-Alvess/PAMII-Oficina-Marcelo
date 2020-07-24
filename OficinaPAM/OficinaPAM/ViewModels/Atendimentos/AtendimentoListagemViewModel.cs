using OficinaPAM.Models;
using OficinaPAM.Servicos.Atendimentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace OficinaPAM.ViewModels.Atendimentos
{
    public class AtendimentoListagemViewModel : BaseViewModel
    {
        private IAtendimentoService aService = new AtendimentoService();
        
        private Atendimento atendimentoSelecionadoPesquisa;
        private Atendimento atendimentoSelecionadoOpces;

        public ICommand NovoCommand { get; set; }
        public ICommand EliminarCommand { get; set; }

        public AtendimentoListagemViewModel()
        {
            Atendimentos = new ObservableCollection<Atendimento>();
            RegistrarCommands();
        }
        private void RegistrarCommands()
        {
            NovoCommand = new Command(() =>
            {
                MessagingCenter.Send<Atendimento>(new Atendimento(), "Mostrar");
            });
            EliminarCommand = new Command<Atendimento>((atendimento) =>
            {
                MessagingCenter.Send<Atendimento>(atendimento, "Confirmação");
            });
        }

        public ObservableCollection<Atendimento> Atendimentos
        {
            get; set;
        }

        public Atendimento AtendimentoSelecionadoOpces
        {
            get => atendimentoSelecionadoOpces;
            set
            {
                if (value != null)
                {
                    atendimentoSelecionadoOpces = value;
                    MessagingCenter.Send<Atendimento>(atendimentoSelecionadoOpces, "MostrarOpcoes");
                }
            }
        }

        public Atendimento AtendimentoSelecionadoPesquisa
        {
            get { return atendimentoSelecionadoPesquisa; }
            set
            {
                if (value != null)
                {
                    atendimentoSelecionadoPesquisa = value;
                    MessagingCenter.Send<Atendimento>(atendimentoSelecionadoPesquisa, "Mostrar");
                }
            }
        }

        public async Task ObterAtendimentosAsync()
        {
            Atendimentos = await aService.GetAtendimentosAsync();

            foreach (Atendimento a in Atendimentos)
            {
                if (a.ClienteID != null)
                    a.Cliente = await CarregarCliente(a.ClienteID.Value);
            }
            OnPropertyChanged(nameof(Atendimentos));
        }

        public async Task ObterCliente(int clienteId)
        {
            AtendimentoSelecionadoOpces.Cliente = await new Servicos.Clientes.ClienteService().GetClienteAsync(clienteId);
        }

        public async Task<Cliente> CarregarCliente(int clienteId)
        {
            return await new Servicos.Clientes.ClienteService().GetClienteAsync(clienteId);
        }

        public async Task EliminarAtendimento(int atendimentoID)
        {
            await aService.DeleteAtendimentoAsync(atendimentoID);
        }

        public async Task RegistrarEntregaAsync(Atendimento atendimento)
        {
            atendimento.DataHoraEntrega = DateTime.Now;
            var indiceAtendimento = Atendimentos.IndexOf(atendimento);
            await aService.PostAtendimentoAsync(atendimento);
            Atendimentos.RemoveAt(indiceAtendimento);
            Atendimentos.Insert(indiceAtendimento, atendimento);
        }

        public async Task DesfazerEntregaAsync(Atendimento atendimento)
        {
            atendimento.DataHoraEntrega = null;
            var indiceAtendimento = Atendimentos.IndexOf(atendimento);
            await aService.PostAtendimentoAsync(atendimento);
            Atendimentos.RemoveAt(indiceAtendimento);
            Atendimentos.Insert(indiceAtendimento, atendimento);
        }
        

    }
}
