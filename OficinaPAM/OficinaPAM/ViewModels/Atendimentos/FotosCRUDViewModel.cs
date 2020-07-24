using OficinaPAM.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OficinaPAM.ViewModels.Atendimentos
{
    public class FotosCRUDViewModel : BaseViewModel
    {
        public AtendimentoFoto AtendimentoFoto { get; set; }
        public ICommand CameraCommand { get; set; }
        public ICommand AlbumCommand { get; set; }
        public ICommand GravarFotoCommand { get; set; }

        public FotosCRUDViewModel(AtendimentoFoto atendimentoFoto)
        {
            this.AtendimentoFoto = atendimentoFoto;
            RegistrarCommands();
        }
        private void RegistrarCommands()
        {
            CameraCommand = new Command(() =>
            {
                MessagingCenter.Send<AtendimentoFoto>(this.AtendimentoFoto, "Camera");
            });

            AlbumCommand = new Command(() =>
            {
                MessagingCenter.Send<AtendimentoFoto>(this.AtendimentoFoto, "Album");

            });

            GravarFotoCommand = new Command(async () =>
            {
                string url = "http://oficinamarcelo.somee.com/api/AtendimentoFotos/FileUpload";
                //TODO: xyz será o endereço da sua API

                AtendimentoFoto afImagem = new AtendimentoFoto()
                {
                    ConteudoFoto = AtendimentoFoto.ConteudoFoto,
                    AtendimentoID = (AtendimentoFoto.AtendimentoID == null) ? 1 : AtendimentoFoto.AtendimentoID,
                    CaminhoFoto = AtendimentoFoto.CaminhoFoto,
                    NomeArquivo = AtendimentoFoto.NomeArquivo,
                    Observacoes = AtendimentoFoto.Observacoes
                };
                //TODO: haverá mais código para envio da Foto para a API
                
                HttpClient client = new HttpClient();
                MultipartFormDataContent content = new MultipartFormDataContent();

                ByteArrayContent baContent = new ByteArrayContent(afImagem.ConteudoFoto);
                StringContent atendimentoIdContent = new StringContent(afImagem.AtendimentoID.ToString());
                StringContent caminhoFotoContent = new StringContent(afImagem.CaminhoFoto);
                StringContent observacoesContent = new StringContent(afImagem.Observacoes);

                content.Add(baContent, afImagem.CaminhoFoto, afImagem.NomeArquivo);
                content.Add(atendimentoIdContent, "AtendimentoID");
                content.Add(caminhoFotoContent, "CaminhoFoto");
                content.Add(observacoesContent, "Observacoes");

                //upload MultipartFormDataContent content async and store response in response var
                var response = await client.PostAsync(url, content);

                //read response result as a string async into json var
                var responsestr = response.Content.ReadAsStringAsync().Result;

                int id = 0;
                int.TryParse(responsestr.Replace("\"", "").Replace(@"\", ""), out id);

                if (id != 0)
                {
                    AtendimentoFoto = new AtendimentoFoto();
                    OnPropertyChanged(nameof(Observacoes));                    
                    MessagingCenter.Send<string>("Dados gravados com sucesso.", "InformacaoCRUD");                    
                }

            });
        }

        public string CaminhoFoto
        {
            get { return this.AtendimentoFoto.CaminhoFoto; }
            set
            {
                this.AtendimentoFoto.CaminhoFoto = value;
                OnPropertyChanged();
            }
        }

        public string Observacoes
        {
            get { return this.AtendimentoFoto.Observacoes; }
            set
            {
                this.AtendimentoFoto.Observacoes = value;
                OnPropertyChanged();
            }
        }

        public string NomeArquivo
        {
            get { return this.AtendimentoFoto.NomeArquivo; }
            set
            {
                this.AtendimentoFoto.NomeArquivo = value;
                OnPropertyChanged();
            }
        }

        public byte[] ConteudoFoto
        {
            get { return this.AtendimentoFoto.ConteudoFoto; }
            set
            {
                this.AtendimentoFoto.ConteudoFoto = value;
                OnPropertyChanged();
            }
        }



    }
}
