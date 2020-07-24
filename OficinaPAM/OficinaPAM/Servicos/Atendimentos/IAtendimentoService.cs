using OficinaPAM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace OficinaPAM.Servicos.Atendimentos
{
    public interface IAtendimentoService
    {
        Task<ObservableCollection<Atendimento>> GetAtendimentosAsync();

        Task<Atendimento> PostAtendimentoAsync(Atendimento a);

        Task<Atendimento> PutAtendimentoAsync(Atendimento a);

        Task<Atendimento> DeleteAtendimentoAsync(int AtendimentoId);
    }
}
