using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OficinaPAM.Interface.Fotos;
using System.IO;

namespace OficinaPAM.Droid.Fotos
{
    public class FotoLoadMediaPlugin : IFotoLoadMediaPlugin
    {
        public string SetPathToPhoto(string caminhoCompleto)
        {
            return caminhoCompleto;            
        }
        public string GetDevicePathToPhoto()
        {
            return 
                Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures), "Fotos");
        }
        public string GetPathToPhoto(string caminhoArmazenado)
        {
            return caminhoArmazenado;            
        }
    }
}

