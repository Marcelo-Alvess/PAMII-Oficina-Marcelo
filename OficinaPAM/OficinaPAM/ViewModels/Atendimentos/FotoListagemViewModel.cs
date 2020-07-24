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
    public class FotoListagemViewModel : BaseViewModel
    {
        private IAtendimentoFotoService fotoService = new AtendimentoFotoService();
        private AtendimentoFoto AtendimentoFoto { get; set; }
        public ICommand NovoCommand { get; set; }

        public FotoListagemViewModel()
        {
            this.Atendimento = new Atendimento();
            AtendimentoFotos = new ObservableCollection<AtendimentoFoto>();
            RegistrarCommands();
        }

        private Atendimento Atendimento { get; set; }

        public FotoListagemViewModel(Atendimento atendimento)
        {
            this.Atendimento = atendimento;
            AtendimentoFotos = new ObservableCollection<AtendimentoFoto>();
            RegistrarCommands();
        }
        private void RegistrarCommands()
        {
            NovoCommand = new Command(() =>
            {
                var atendimentoFoto = new AtendimentoFoto() { AtendimentoID = this.Atendimento.AtendimentoID };
                MessagingCenter.Send<AtendimentoFoto>(atendimentoFoto, "Mostrar");
            }
            ,() =>
            {
                return !this.Atendimento.EstaFinalizado;
            });
        }
        public ObservableCollection<AtendimentoFoto> AtendimentoFotos
        {
            get; set;
        }

        public async Task ObterAtendimentoFotosAsync()
        {
            if (this.Atendimento.AtendimentoID != null)
            {
                AtendimentoFotos = await fotoService.GetAtendimentoFotosAsync(this.Atendimento.AtendimentoID.Value);
            }
            else
            {
                AtendimentoFotos = await fotoService.GetAtendimentoFotosAsync();
            }
            OnPropertyChanged(nameof(AtendimentoFotos));
        }

    }
}
