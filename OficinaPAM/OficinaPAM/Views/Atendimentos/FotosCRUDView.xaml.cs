using OficinaPAM.Interface.Fotos;
using OficinaPAM.Models;
using OficinaPAM.ViewModels.Atendimentos;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OficinaPAM.Views.Atendimentos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FotosCRUDView : ContentPage
    {
        private FotosCRUDViewModel viewModel;
        public FotosCRUDView()
        {
            InitializeComponent();
        }

        public FotosCRUDView(AtendimentoFoto foto, string title) : this()
        {            
            BindingContext = viewModel = new FotosCRUDViewModel(foto);
            this.Title = title;            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<AtendimentoFoto>(this, "Camera", async (foto) => { await TirarFotoAsync(foto); });
            MessagingCenter.Subscribe<AtendimentoFoto>(this, "Album", async (foto) => { await SelecionarFotoDoAlbumAsync(foto); });

            MessagingCenter.Subscribe<string>(this, "InformacaoCRUD", async (msg) =>
            { 
                await DisplayAlert("Informação", msg, "OK"); 
            });
            
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<AtendimentoFoto>(this, "Camera");
            MessagingCenter.Unsubscribe<AtendimentoFoto>(this, "Album");
            MessagingCenter.Unsubscribe<AtendimentoFoto>(this, "InformacaoCRUD");
        }


        private async Task<bool> TirarFotoAsync(AtendimentoFoto foto)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Sem Câmera", "A câmera não está disponível.", "OK");
                await Task.FromResult(false);
            }

            string fileName;
            if (foto.CaminhoFoto == null)
            {
                fileName = String.Format("{0:ddMMyyy_HHmm}", DateTime.Now) + ".jpg";
            }
            else
            {
                File.Delete(DependencyService.Get<IFotoLoadMediaPlugin>().GetPathToPhoto(foto.CaminhoFoto));
                fileName = (foto.CaminhoFoto.LastIndexOf("/") > 0) ?
                    foto.CaminhoFoto.Substring(foto.CaminhoFoto.LastIndexOf("/") + 1) :
                    String.Format("{0:ddMMyyy_HHmm}", DateTime.Now) + ".jpg";
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Fotos",
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                Name = fileName
            });

            if (file == null)
                return await Task.FromResult(false); ;

            fotoCarro.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            viewModel.CaminhoFoto = file.Path;
            viewModel.NomeArquivo = fileName;

            MemoryStream ms = null;
            using (ms = new MemoryStream())
            {
                var stream = file.GetStream();
                stream.CopyTo(ms);
            }
            viewModel.ConteudoFoto = ms.ToArray();

            return await Task.FromResult(true);


        }

        public string SaveFotoFromAlbum(string caminhoFoto, Plugin.Media.Abstractions.MediaFile file)
        {
            string nomeArquivo;

            if (caminhoFoto == null || caminhoFoto.StartsWith("http"))
            {
                nomeArquivo = String.Format("{0:ddMMyyy_HHmm}", DateTime.Now) + ".jpg";
            }
            else
            {
                if (File.Exists(DependencyService.Get<IFotoLoadMediaPlugin>().GetPathToPhoto(caminhoFoto)))
                    File.Delete(DependencyService.Get<IFotoLoadMediaPlugin>().GetPathToPhoto(caminhoFoto));

                nomeArquivo = (caminhoFoto.LastIndexOf("/") > 0) ? caminhoFoto.Substring(caminhoFoto.LastIndexOf("/") + 1) : caminhoFoto;
            }
            
            viewModel.NomeArquivo = nomeArquivo;
            return file.Path;
        }

       
        private async Task SelecionarFotoDoAlbumAsync(AtendimentoFoto foto)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Álbum não suportado",
                    "Não existe permissão para acessar o álbum de fotos", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return;
            var imagePath = SaveFotoFromAlbum(foto.CaminhoFoto, file);

            fotoCarro.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            viewModel.CaminhoFoto = imagePath;

            MemoryStream ms = null;
            using (ms = new MemoryStream())
            {
                var stream = file.GetStream();
                stream.CopyTo(ms);
            }
            viewModel.ConteudoFoto = ms.ToArray();
            return;
        }


        //await CrossMedia.Current.Initialize();
        //if (!CrossMedia.Current.IsPickPhotoSupported)
        //{
        //    await DisplayAlert("Álbum não suportado",
        //        "Não existe permissão para acessar o álbum de fotos", "OK");
        //    return;
        //}
        //var file = await CrossMedia.Current.PickPhotoAsync();
        //if (file == null)
        //    return;
        //var imagePath = SaveFotoFromAlbum(foto.CaminhoFoto, file);
        //fotoCarro.Source = ImageSource.FromStream(() =>
        //{
        //    var stream = file.GetStream();
        //    return stream;
        //});
        //viewModel.CaminhoFoto = imagePath;
        //return;



    }
}