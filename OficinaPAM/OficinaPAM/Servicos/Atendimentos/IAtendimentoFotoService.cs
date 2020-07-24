using OficinaPAM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace OficinaPAM.Servicos.Atendimentos
{
    public interface IAtendimentoFotoService
    {
        Task<ObservableCollection<AtendimentoFoto>> GetAtendimentoFotosAsync(int atendimentoId);
        Task<ObservableCollection<AtendimentoFoto>> GetAtendimentoFotosAsync();
        Task<AtendimentoFoto> PostAtendimentoFotoAsync(AtendimentoFoto f);
        Task<AtendimentoFoto> PutAtendimentoFotoAsync(AtendimentoFoto f);
        Task<AtendimentoFoto> DeleteAtendimentoFotoAsync(int AtendimentoFotoId);
    }
}
