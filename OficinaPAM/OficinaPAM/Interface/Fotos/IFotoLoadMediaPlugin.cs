using System;
using System.Collections.Generic;
using System.Text;

namespace OficinaPAM.Interface.Fotos
{
    public interface IFotoLoadMediaPlugin
    {
        string SetPathToPhoto(string caminhoCompleto);
        string GetDevicePathToPhoto();
        string GetPathToPhoto(string caminhoArmazenado);
    }
}
